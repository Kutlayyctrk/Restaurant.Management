using Microsoft.EntityFrameworkCore;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Domain.Enums;
using Project.Persistance.ContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.Repositories
{
    public class OrderRepository(MyContext myContext) : BaseRepository<Order>(myContext), IOrderRepository
    {
        private readonly MyContext _context = myContext;

        public async Task CloseOrderState(int orderId)
        {
          Order order= await _context.Orders.FirstOrDefaultAsync(o=>o.Id == orderId);

            if(order!=null)
            {
                if(order.OrderState!=null)
                {
                    order.OrderState = OrderStatus.Closed;
                    order.WaiterId= null;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetActiveOrderByTableIdAsync(int tableId)
        {
            return await _context.Orders
           .Include(x => x.OrderDetails)
           .ThenInclude(x => x.Product)
           .FirstOrDefaultAsync(x => x.TableId == tableId && x.OrderState == OrderStatus.SentToKitchen );
        }

        public async Task<List<Order>> GetACtiveSaleOrderForWaiterAsync()
        {
            return await _context.Set<Order>()

         .Include(o => o.OrderDetails.Where(od => od.DetailState == OrderDetailStatus.SendToKitchen && od.DetailState== OrderDetailStatus.SendToTheTable))
             .ThenInclude(od => od.Product)
         .Include(o => o.Supplier)
         .Where(o =>
             (o.OrderState != OrderStatus.Closed)
             && o.Type == OrderType.Sale)
         .ToListAsync();
        }

        public async Task<List<Order>> GetActiveSaleOrdersForKitchenAndBarAsync()
        {
            return await _context.Set<Order>()
         
         .Include(o => o.OrderDetails.Where(od => od.DetailState == OrderDetailStatus.SendToKitchen))
             .ThenInclude(od => od.Product)
         .Include(o => o.Supplier)
         .Where(o =>
             (o.OrderState == OrderStatus.SentToKitchen )
             && o.Type == OrderType.Sale)
         .ToListAsync();
        }

        public async Task UpdateOrderStateAsync(int orderId, OrderDetailStatus newDetailState)
        {
           
            Order order = await _context.Set<Order>()
        .Include(o => o.OrderDetails)
        .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order != null)
            {
                if (order.OrderDetails != null)
                {
                    foreach (OrderDetail detail in order.OrderDetails)
                    {
                        detail.DetailState = newDetailState;
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
