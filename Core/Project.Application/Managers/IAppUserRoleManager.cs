using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Domain.Entities.Concretes;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IAppUserRoleManager : IManager<AppUserRole, AppUserRoleDTO>
    {
        Task<OperationStatus> HardDeleteByCompositeKeyAsync(int userId, int roleId);
        // Composite key ile AppUserRole hard delete (Identity’den de rolden çıkarır)

        Task<OperationStatus> SoftDeleteByCompositeKeyAsync(int userId, int roleId);
        // Composite key ile AppUserRole soft delete (Status = Deleted işaretler)

        Task<OperationStatus> UpdateByCompositeKeyAsync(AppUserRoleDTO dto);
        // Composite key ile AppUserRole güncelleme (örneğin UpdatedDate, Status vs.)

    }
}