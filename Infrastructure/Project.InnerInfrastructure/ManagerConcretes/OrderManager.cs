using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;

using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class OrderManager(IOrderRepository orderRepository, IMapper mapper, IValidator<OrderDTO> orderValidator, IStockTransActionManager stockTransActionManager) : BaseManager<Order, OrderDTO>(orderRepository, mapper, orderValidator), IOrderManager
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<OrderDTO> _validator = orderValidator;
        private readonly IStockTransActionManager _stockTransActionManager = stockTransActionManager;
        public async Task ChangeOrderStateAsync(int orderId, OrderStatus newState)
        {
            await _orderRepository.UpdateOrderStateAsync(orderId, newState);


        }

        public async Task<List<OrderDTO>> GetActiveSaleOrdersAsync()
        {
            List<Order> orders = await _orderRepository.GetActiveSaleOrdersAsync();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public override async Task<OperationStatus> UpdateAsync(OrderDTO originalDto, OrderDTO newDto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(newDto);
            if (!validationResult.IsValid) return OperationStatus.Failed;

            Order originalEntity = await _orderRepository.GetQuery()
                                             .Include(x => x.OrderDetails)
                                             .FirstOrDefaultAsync(x => x.Id == originalDto.Id);

            if (originalEntity == null) return OperationStatus.NotFound;

            List<OrderDetail> newlyAddedDetails = new List<OrderDetail>();

            
            originalEntity.SupplierId = newDto.SupplierId;
            originalEntity.TableId = newDto.TableId;
            originalEntity.WaiterId = newDto.WaiterId;
            originalEntity.OrderDate = newDto.OrderDate;
            originalEntity.TotalPrice = newDto.TotalPrice;
            originalEntity.Type = newDto.Type;

            originalEntity.Status = DataStatus.Updated;
            originalEntity.UpdatedDate = DateTime.Now;

         
            List<OrderDetail> detailsRemove = originalEntity.OrderDetails
                .Where(z => !newDto.OrderDetails.Any(x => x.Id == z.Id)).ToList();

            foreach (OrderDetail toRemove in detailsRemove)
            {
                await _stockTransActionManager.CreateDeletionOrderActionAsync(toRemove, originalEntity.Type);
                originalEntity.OrderDetails.Remove(toRemove);
            }

      
            foreach (OrderDetailDTO dto in newDto.OrderDetails)
            {
                OrderDetail existingDetail = originalEntity.OrderDetails
                    .FirstOrDefault(x => x.Id == dto.Id && x.Id != 0);

                if (existingDetail != null)
                {
               
                    decimal oldQuantity = existingDetail.Quantity;

                
                    decimal quantityDiff = dto.Quantity - oldQuantity;

                    if (quantityDiff != 0)
                    {
                       
                        await _stockTransActionManager.CreateUpdateOrderActionAsync(existingDetail, quantityDiff, originalEntity.Type);
                    }

                 
                    _mapper.Map(dto, existingDetail);
                    existingDetail.Status = DataStatus.Updated;
                    existingDetail.UpdatedDate = DateTime.Now;
                }
                else
                {
                    OrderDetail newDetail = _mapper.Map<OrderDetail>(dto);
                    newDetail.OrderId = originalEntity.Id;
                    newDetail.Status = DataStatus.Inserted;
                    newDetail.UpdatedDate = DateTime.Now;

                    originalEntity.OrderDetails.Add(newDetail);
                    newlyAddedDetails.Add(newDetail);
                }
            }

            await _orderRepository.UpdateAsync(originalEntity);

            foreach (OrderDetail freshDetail in newlyAddedDetails)
            {
                await _stockTransActionManager.CreateInitialOrderActionAsync(freshDetail, originalEntity.Type);
            }

            return OperationStatus.Success;
        }

        public async override Task<OperationStatus> CreateAsync(OrderDTO dto)
        {
          
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid) return OperationStatus.Failed;

         
            Order order = _mapper.Map<Order>(dto);
            order.Status = DataStatus.Inserted;
            order.InsertedDate = DateTime.Now;

          
            foreach (OrderDetail detail in order.OrderDetails)
            {
                detail.Status = DataStatus.Inserted;
                detail.InsertedDate = DateTime.Now;
            }

          
            await _orderRepository.CreateAsync(order);

        
            foreach (OrderDetail detail in order.OrderDetails)
            {
              
                await _stockTransActionManager.CreateInitialOrderActionAsync(detail, order.Type);
            }

            return OperationStatus.Success; 
        }
    }
}
