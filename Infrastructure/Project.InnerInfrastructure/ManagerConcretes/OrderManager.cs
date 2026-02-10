using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;
using Project.Application.Results;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class OrderManager(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<OrderDTO> orderValidator, IStockTransActionManager stockTransActionManager, IProductManager productManager, ILogger<OrderManager> logger) : BaseManager<Order, OrderDTO>(orderRepository, unitOfWork, mapper, orderValidator, logger), IOrderManager
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<OrderDTO> _validator = orderValidator;
        private readonly IStockTransActionManager _stockTransActionManager = stockTransActionManager;
        private readonly IProductManager _productManager = productManager;

        public async Task ChangeOrderStateAsync(int orderId, OrderDetailStatus newState)
        {
            await _orderRepository.UpdateOrderStateAsync(orderId, newState);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<OrderDTO>> GetActiveSaleOrdersForKitchenAndBarAsync()
        {
            List<Order> orders = await _orderRepository.GetActiveSaleOrdersForKitchenAndBarAsync();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public override async Task<Result> UpdateAsync(OrderDTO originalDto, OrderDTO newDto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(newDto);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Failure(OperationStatus.ValidationError, errors);
            }

            Order originalEntity = await _orderRepository.GetQuery()
                                             .Include(x => x.OrderDetails)
                                             .FirstOrDefaultAsync(x => x.Id == originalDto.Id);

            if (originalEntity == null) return Result.Failure(OperationStatus.NotFound, "Sipariş bulunamadı.");

            List<OrderDetail> newlyAddedDetails = new();

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
            await _unitOfWork.CommitAsync();

            foreach (OrderDetail freshDetail in newlyAddedDetails)
            {
                await _stockTransActionManager.CreateInitialOrderActionAsync(freshDetail, originalEntity.Type);
            }

            return Result.Succeed("Sipariş güncellendi.");
        }

        public async override Task<Result> CreateAsync(OrderDTO dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Failure(OperationStatus.ValidationError, errors);
            }

            if (dto.OrderDetails == null || dto.OrderDetails.Count == 0)
            {
                return Result.Failure(OperationStatus.Failed, "Sipariş detayları boş olamaz.");
            }

            Order order = _mapper.Map<Order>(dto);
            order.Status = DataStatus.Inserted;
            order.InsertedDate = DateTime.Now;

            foreach (OrderDetail detail in order.OrderDetails)
            {
                detail.Status = DataStatus.Inserted;
                detail.InsertedDate = DateTime.Now;
            }

            await _orderRepository.CreateAsync(order);
            int commitResult = await _unitOfWork.CommitAsync();
            if (commitResult <= 0)
            {
                return Result.Failure(OperationStatus.Failed, "Sipariş kaydedilemedi.");
            }

            Order persistedOrder = await _orderRepository.GetQuery()
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == order.Id);

            if (persistedOrder == null)
            {
                return Result.Failure(OperationStatus.Failed, "Sipariş doğrulanamadı.");
            }

            foreach (OrderDetail detail in persistedOrder.OrderDetails)
            {
                await _stockTransActionManager.CreateInitialOrderActionAsync(detail, persistedOrder.Type);

                if (persistedOrder.Type == OrderType.Purchase)
                {
                    await _productManager.IncreaseStockAsync(detail.ProductId, detail.Quantity);
                }
                else if (persistedOrder.Type == OrderType.Sale)
                {
                    await _productManager.ReduceStockAfterSaleAsync(detail.ProductId, detail.Quantity, detail.Id);
                }
            }

            return Result.Succeed("Sipariş oluşturuldu.");
        }

        public async Task<OrderDTO?> GetActiveOrderForTableAsync(int tableId)
        {
            Order order = await _orderRepository.GetActiveOrderByTableIdAsync(tableId);
            return order != null ? _mapper.Map<OrderDTO>(order) : null;
        }

        public async Task CloseOrderState(int orderId)
        {
            await _orderRepository.CloseOrderState(orderId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<OrderDTO>> GetACtiveSaleOrderForWaiterAsync()
        {
            List<Order> orders = await _orderRepository.GetACtiveSaleOrderForWaiterAsync();
            return _mapper.Map<List<OrderDTO>>(orders);
        }
    }
}