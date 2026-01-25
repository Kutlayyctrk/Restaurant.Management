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
    public class OrderManager(IOrderRepository orderRepository, IMapper mapper, IValidator<OrderDTO> orderValidator) : BaseManager<Order, OrderDTO>(orderRepository, mapper, orderValidator), IOrderManager
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<OrderDTO> _validator = orderValidator;
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
            if (!validationResult.IsValid)
            { 
                return OperationStatus.Failed;
            }

            Order originalEntity = await _orderRepository.GetQuery().Include(x => x.OrderDetails).FirstOrDefaultAsync(x => x.Id == originalDto.Id);

            if (originalEntity == null)
            {
                return OperationStatus.NotFound;
            }
            _mapper.Map(newDto, originalEntity);
            originalEntity.Status = DataStatus.Updated;
            originalEntity.UpdatedDate = DateTime.Now;

            var detailsRemove = originalEntity.OrderDetails.Where(z => !newDto.OrderDetails.Any(x => x.Id == z.Id)).ToList();

            foreach (var toRemove in detailsRemove)
            {
                originalEntity.OrderDetails.Remove(toRemove);
            }

            foreach (var dto in   newDto.OrderDetails)
            {
                var existingDetail = originalEntity.OrderDetails.FirstOrDefault(x => x.Id == dto.Id && x.Id != 0);
                if(existingDetail!=null)
                {
                    _mapper.Map(dto, existingDetail);
                    existingDetail.Status = DataStatus.Updated;
                    existingDetail.UpdatedDate = DateTime.Now;
                }
                else
                {
                    var newDetail = _mapper.Map<OrderDetail>(dto);
                    newDetail.OrderId = originalEntity.Id;
                    newDetail.Status = DataStatus.Updated;
                    newDetail.UpdatedDate = DateTime.Now;
                    originalEntity.OrderDetails.Add(newDetail);
                }
            }

            await _orderRepository.UpdateAsync(originalEntity);
            return OperationStatus.Success;

        }
    }
}
