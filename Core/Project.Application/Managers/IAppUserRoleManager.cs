using Project.Application.DTOs;
using Project.Domain.Entities.Concretes;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IAppUserRoleManager : IManager<AppUserRole, AppUserRoleDTO>
    {
        Task<string> RemoveByUserAndRoleAsync(int userId, int roleId);
    }
}
