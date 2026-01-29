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
        /// <summary>
        /// Sadece aktif ve satış tipindeki siparişleri getirir.
        /// </summary>
        Task<List<OrderDTO>> GetActiveSaleOrdersForKitchenAndBarAsync();
        Task ChangeOrderStateAsync(int orderId, OrderDetailStatus newState); //Order durumunu güncellemek için yazıldı
        Task<OrderDTO?> GetActiveOrderForTableAsync(int tableId);
        Task CloseOrderState(int orderId);
        Task<List<OrderDTO>> GetACtiveSaleOrderForWaiterAsync();
    }

}

