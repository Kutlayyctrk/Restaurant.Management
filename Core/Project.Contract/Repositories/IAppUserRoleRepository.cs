using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contract.Repositories
{
    public interface IAppUserRoleRepository : IRepository<AppUserRole>
    {
       
        Task<AppUserRole> GetByCompositeKeyAsync(int userId, int roleId);
        Task<int> DeleteByUserAndRoleAsync(int userId, int roleId);
        AppUserRole GetLocalTrackedEntity(int userId, int roleId);

    }
}
