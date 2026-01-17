using Project.Application.DTOs;
using Project.Domain.Entities.Concretes;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IAppUserRoleManager : IManager<AppUserRole, AppUserRoleDTO>
    {
        Task<string> HardDeleteByCompositeKeyAsync(int userId, int roleId);
        Task<string> SoftDeleteByCompositeKeyAsync(int userId, int roleId);
    }
}