using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Domain.Entities.Concretes;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IAppUserRoleManager : IManager<AppUserRole, AppUserRoleDTO>
    {
        Task<OperationStatus> HardDeleteByCompositeKeyAsync(int userId, int roleId);
        // Composite key ile AppUserRole hard delete 

        Task<OperationStatus> SoftDeleteByCompositeKeyAsync(int userId, int roleId);
        // Composite key ile AppUserRole soft delete

        Task<OperationStatus> UpdateByCompositeKeyAsync(AppUserRoleDTO dto);
        // Composite key ile AppUserRole güncelleme 
        Task<AppUserRoleDTO> GetByCompositeKeyAsync(int userId, int roleId);
        // Composite key ile AppUserRole getirme
        Task<IList<int>> GetRoleIdsByUserIdAsync(int userId);
    }
}

// TODO: Metotların summarykeri ekleneecek.