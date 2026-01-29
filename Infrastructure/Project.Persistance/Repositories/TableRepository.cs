using Microsoft.EntityFrameworkCore;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Persistance.ContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.Repositories
{
    public class TableRepository(MyContext myContext) : BaseRepository<Table>(myContext), ITableRepository
    {
        private readonly MyContext _myContext = myContext;
        public async Task<List<Table>> GetTablesByUserIdAsync(string userId)
        {
            if (!int.TryParse(userId, out int waiterId))
                return new List<Table>();

            return await _myContext.Tables
        .Where(x => x.WaiterId == null || x.WaiterId == waiterId)
        .ToListAsync();
        }
    }
}
