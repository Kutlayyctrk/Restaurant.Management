using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contract.Repositories
{
    public interface IOrderRepository:IRepository<Order>

    {
        Task<List<Order>> GetActiveOrdersAsync();
        Task UpdateOrderStateAsync(int orderId, OrderStatus newState);

    }
}
