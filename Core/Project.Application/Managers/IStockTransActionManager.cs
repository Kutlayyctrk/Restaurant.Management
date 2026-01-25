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
    public interface IStockTransActionManager:IManager<StockTransAction,StockTransActionDTO>
    {
        Task CreateInitialOrderActionAsync(OrderDetail detail, OrderType type);
        Task CreateUpdateOrderActionAsync(OrderDetail detail, decimal quantityDiff, OrderType type);
        Task CreateDeletionOrderActionAsync(OrderDetail detail, OrderType type);
        Task CreateManualActionAsync(StockTransActionDTO dto);
    }
}
