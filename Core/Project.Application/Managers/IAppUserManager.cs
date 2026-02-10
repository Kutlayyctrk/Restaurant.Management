using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Results;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IAppUserManager : IManager<AppUser, AppUserDTO>
    {
        Task<Result> LoginAsync(AppUserDTO dto);
        Task LogoutAsync();
        Task<Result> ConfirmEmailAsync(string userId, string token);
        Task<List<AppUserDTO>> GetConfirmedUsersAsync();
    }
}
