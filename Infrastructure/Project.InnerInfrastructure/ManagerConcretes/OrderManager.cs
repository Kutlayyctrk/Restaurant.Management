using AutoMapper;
using FluentValidation;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class OrderManager(IOrderRepository orderRepository, IMapper mapper, IValidator<OrderDTO> orderValidator) : BaseManager<Order, OrderDTO>(orderRepository, mapper, orderValidator), IOrderManager
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMapper _mapper = mapper;
        public async Task ChangeOrderStateAsync(int orderId, OrderStatus newState)
        {
            await _orderRepository.UpdateOrderStateAsync(orderId, newState);


        }

        public async Task<List<OrderDTO>> GetActiveSaleOrdersAsync()
        {
            List<Order> orders = await _orderRepository.GetActiveSaleOrdersAsync();
            return _mapper.Map<List<OrderDTO>>(orders);
        }
    }
}
