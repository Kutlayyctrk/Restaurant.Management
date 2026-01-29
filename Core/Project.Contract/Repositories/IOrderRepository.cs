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
        /// <summary>
        /// Sadece aktif ve satış tipindeki siparişleri getirir.
        /// </summary>
        Task<List<Order>> GetActiveSaleOrdersForKitchenAndBarAsync();
        Task<List<Order>> GetACtiveSaleOrderForWaiterAsync();
        Task UpdateOrderStateAsync(int orderId, OrderDetailStatus newDetailState);
        Task<Order?> GetActiveOrderByTableIdAsync(int tableId);
        Task CloseOrderState(int orderId);

    }
}
