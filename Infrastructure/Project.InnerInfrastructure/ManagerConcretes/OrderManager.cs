using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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

            Order originalEntity = await _orderRepository.GetQuery()
                                            .Include(x => x.OrderDetails)
                                            .FirstOrDefaultAsync(x => x.Id == originalDto.Id);

            if (originalEntity == null)
            {
                return OperationStatus.NotFound;
            }
            originalEntity.OrderDetails.Clear();

            originalEntity.SupplierId = newDto.SupplierId;
            originalEntity.OrderDate = newDto.OrderDate;
            originalEntity.TotalPrice = newDto.TotalPrice;
            originalEntity.Status = DataStatus.Updated;
            originalEntity.UpdatedDate = DateTime.Now;

            foreach (var detailDto in newDto.OrderDetails)
            {
                var newDetatil = _mapper.Map<OrderDetail>(detailDto);
                newDetatil.OrderId = originalEntity.Id;
                originalEntity.OrderDetails.Add(newDetatil);
            }
            await _orderRepository.UpdateAsync(originalEntity);
            return OperationStatus.Success;

        }
    }
}
