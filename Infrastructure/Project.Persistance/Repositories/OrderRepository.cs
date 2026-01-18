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

        public async Task<List<Order>> GetActiveOrdersAsync()
        {
            return await _context.Set<Order>()
             .Include(o => o.OrderDetails)
                 .ThenInclude(od => od.Product) 
             .Where(o => o.OrderState == OrderStatus.Pending || o.OrderState == OrderStatus.SentToKitchen)
             .ToListAsync();


        }

        public async  Task UpdateOrderStateAsync(int orderId, OrderStatus newState)
        {
            Order order = await _context.Set<Order>().FindAsync(orderId);
            if (order != null)
            {
                order.OrderState = newState;
                _context.Update(order);
                await _context.SaveChangesAsync();
            }

        }
    }
}
