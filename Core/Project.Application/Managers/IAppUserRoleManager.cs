using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Results;
using Project.Domain.Entities.Concretes;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IAppUserRoleManager : IManager<AppUserRole, AppUserRoleDTO>
    {
        Task<Result> HardDeleteByCompositeKeyAsync(int userId, int roleId);
        Task<Result> SoftDeleteByCompositeKeyAsync(int userId, int roleId);
        Task<Result> UpdateByCompositeKeyAsync(AppUserRoleDTO dto);
        Task<AppUserRoleDTO> GetByCompositeKeyAsync(int userId, int roleId);
        Task<IList<int>> GetRoleIdsByUserIdAsync(int userId);
    }
}