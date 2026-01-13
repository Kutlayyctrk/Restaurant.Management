using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class AppUserRoleManager(IAppUserRoleRepository appUserRoleRepository,IMapper mapper,IValidator<AppUserRoleDTO> appUserRoleValidator,UserManager<AppUser> userManager,RoleManager<AppRole> roleManager):BaseManager<AppUserRole,AppUserRoleDTO>(appUserRoleRepository,mapper,appUserRoleValidator),IAppUserRoleManager
    {
      
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly RoleManager<AppRole> _roleManager = roleManager;
        private readonly IAppUserRoleRepository _appUserRoleRepository = appUserRoleRepository;

        public override async Task<string> CreateAsync(AppUserRoleDTO dto)
        {
            AppUser user= await _userManager.FindByIdAsync(dto.UserId.ToString());
            if(user==null)
            {
                return "Kullanıcı eklenemedi";
            }
            AppRole role= await _roleManager.FindByIdAsync(dto.RoleId.ToString());
            if(role==null)
            {
                return "Rol eklenemedi";
            }

            IdentityResult result= await _userManager.AddToRoleAsync(user,role.Name);
            if(!result.Succeeded)
            {
                return string.Join("|",result.Errors.Select(e=>e.Description));
            }
            return "Kullanıcı role başarıyla eklendi";
        }
        public override async Task<string> HardDeleteAsync(int id)
        {
            AppUserRole entity = await _appUserRoleRepository.GetByIdAsync(id);
            if(entity==null)
            {
                return "Silinecek User-Role ilişkisi bulunamadı.";
            }
            AppUser user =await _userManager.FindByIdAsync(entity.UserId.ToString());
            if(user==null)
            {
                return "Kullanıcı bulunamadı";
            }

            AppRole role= await _roleManager.FindByIdAsync(entity.RoleId.ToString());
            if(role==null)
            {
                return "Rol bulunamadı";
            }
            IdentityResult result= await _userManager.RemoveFromRoleAsync(user,role.Name);
            if(!result.Succeeded)
            {
                return string.Join("|",result.Errors.Select(e=>e.Description));
            }
            return "Kullanıcı rolden başarıyla çıkarıldı";
        }
    }
}
