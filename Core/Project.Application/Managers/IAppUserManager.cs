using Project.Application.DTOs;
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
        Task<string> LoginAsync(AppUserDTO dto);
        Task<string> ConfirmEmailAsync(string userId, string token);

    }
}
