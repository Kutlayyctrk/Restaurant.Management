using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IAppUserManager:IManager<AppUser,AppUserDTO>
    {
        Task<OperationStatus> LoginAsync(AppUserDTO dto); //Giriş işlemi için yazıldı
        Task<OperationStatus> ConfirmEmailAsync(string userId, string token); //Mail onaylama işlemi için yazıldı
        Task<List<AppUserDTO>> GetConfirmedUsersAsync();//Maili onaylanmış kullanıcıları getirmek için yazıldı

    }
}
