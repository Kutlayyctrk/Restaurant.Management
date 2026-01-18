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
    public class OrderDetailRepository(MyContext myContext):BaseRepository<OrderDetail>(myContext),IOrderDetailRepository
    {
        private readonly MyContext _context = myContext;
        public async Task UpdateDetailStateAsync(int detailId, OrderDetailStatus newState)
        {
            OrderDetail detail = await _context.Set<OrderDetail>().FindAsync(detailId);
            if (detail != null)
            {
                detail.DetailState = newState;
                await _context.SaveChangesAsync();
            }

        }

    }
}
