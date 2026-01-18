using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Domain.Entities.Concretes;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IAppUserRoleManager : IManager<AppUserRole, AppUserRoleDTO>
    {
        Task<OperationStatus> HardDeleteByCompositeKeyAsync(int userId, int roleId);
        Task<OperationStatus> SoftDeleteByCompositeKeyAsync(int userId, int roleId);
    }
}