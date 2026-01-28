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

        public async Task<Order?> GetActiveOrderByTableIdAsync(int tableId)
        {
            return await _context.Orders
           .Include(x => x.OrderDetails)
           .ThenInclude(x => x.Product)
           .FirstOrDefaultAsync(x => x.TableId == tableId && x.OrderState != OrderStatus.Closed);
        }

        public async Task<List<Order>> GetActiveSaleOrdersAsync()
        {
            return await _context.Set<Order>()
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Include(o => o.Supplier)
                .Where(o =>
                    (o.OrderState == OrderStatus.Pending
                     || o.OrderState == OrderStatus.SentToKitchen
                     || o.OrderState == OrderStatus.Ready)
                    && o.Type == OrderType.Sale)
                .ToListAsync();
        }

        public async  Task UpdateOrderStateAsync(int orderId, OrderStatus newState)
        {
            Order order = await _context.Set<Order>().FindAsync(orderId);
            if (order != null)
            {
                order.OrderState = newState;
                await _context.SaveChangesAsync();
            }


        }
    }
}
