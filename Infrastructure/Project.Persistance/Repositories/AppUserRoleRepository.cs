
using Microsoft.EntityFrameworkCore;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using Project.Persistance.ContextClasses;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Project.Persistance.Repositories
{
    public class AppUserRoleRepository : BaseRepository<AppUserRole>, IAppUserRoleRepository
    {
        private readonly MyContext _myContext;

        public AppUserRoleRepository(MyContext myContext) : base(myContext)
        {
            _myContext = myContext;
        }

        public async Task<AppUserRole> GetByUserAndRoleAsync(int userId, int roleId)
        {
            List<AppUserRole> list = await WhereAsync(x => x.UserId == userId && x.RoleId == roleId);
            return list.FirstOrDefault();
        }

        public override async Task<AppUserRole> GetByIdAsync(int id)
        {
            List<AppUserRole> list = await WhereAsync(x => x.Id == id);
            return list.FirstOrDefault();
        }


        public async Task<int> DeleteByUserAndRoleAsync(int userId, int roleId)
        {
            return await _myContext.Set<AppUserRole>()
                                   .Where(x => x.UserId == userId && x.RoleId == roleId)
                                   .ExecuteDeleteAsync();
        }

        public async Task<AppUserRole> GetByCompositeKeyAsync(int userId, int roleId)
        {
            return await _myContext.AppUserRoles
                                   .FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == roleId);
        }
        public AppUserRole GetLocalTrackedEntity(int userId, int roleId)
        {
            return _myContext.AppUserRoles.Local
                .FirstOrDefault(x => x.UserId == userId && x.RoleId == roleId);
        }


    }
}