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
        // userId ve roleId ile AppUserRole bulma

        Task<int> DeleteByUserAndRoleAsync(int userId, int roleId);
        // userId ve roleId ile AppUserRole hard delete (kalıcı silme)

        AppUserRole GetLocalTrackedEntity(int userId, int roleId);
        // EF Core ChangeTracker içinde aynı entity var mı kontrolü (duplicate eklememek için)

        Task UpdateByCompositeKeyAsync(AppUserRole entity);
        // Composite key ile AppUserRole güncelleme (soft delete veya update için)


    }
}
