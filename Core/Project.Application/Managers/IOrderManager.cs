using Project.Application.DTOs;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IOrderManager:IManager<Order,OrderDTO>
    {
        Task<List<OrderDTO>> GetActiveOrdersAsync(); 
        Task ChangeOrderStateAsync(int orderId, OrderStatus newState);
    }

}

