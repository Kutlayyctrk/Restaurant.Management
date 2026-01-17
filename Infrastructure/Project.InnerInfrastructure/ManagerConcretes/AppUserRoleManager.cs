
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class AppUserRoleManager(IAppUserRoleRepository appUserRoleRepository, IMapper mapper, IValidator<AppUserRoleDTO> appUserRoleValidator, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        : BaseManager<AppUserRole, AppUserRoleDTO>(appUserRoleRepository, mapper, appUserRoleValidator), IAppUserRoleManager
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly RoleManager<AppRole> _roleManager = roleManager;
        private readonly IAppUserRoleRepository _appUserRoleRepository = appUserRoleRepository;
        private readonly IMapper _mapper = mapper;

        public override async Task<string> CreateAsync(AppUserRoleDTO dto)
        {
            AppUser user = await _userManager.FindByIdAsync(dto.UserId.ToString());
            if (user == null) return "Kullanıcı bulunamadı";

            AppRole role = await _roleManager.FindByIdAsync(dto.RoleId.ToString());
            if (role == null) return "Rol bulunamadı";

         
            AppUserRole existing = await _appUserRoleRepository.GetByCompositeKeyAsync(dto.UserId, dto.RoleId);
            if (existing != null) return "Bu kullanıcı zaten bu role atanmış.";

           
            IdentityResult result = await _userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
                return string.Join("|", result.Errors.Select(e => e.Description));

           
            AppUserRole domainEntity = _mapper.Map<AppUserRole>(dto);
            domainEntity.InsertedDate = DateTime.Now;
            domainEntity.Status = Project.Domain.Enums.DataStatus.Inserted;

      
            AppUserRole trackedEntity = _appUserRoleRepository.GetLocalTrackedEntity(dto.UserId, dto.RoleId);
            if (trackedEntity == null)
            {
                await _appUserRoleRepository.CreateAsync(domainEntity);
            }

            return "Kullanıcı role başarıyla eklendi";


        }


       

        public async Task<string> HardDeleteByCompositeKeyAsync(int userId, int roleId)
        {
            AppUserRole entity = await _appUserRoleRepository.GetByCompositeKeyAsync(userId, roleId);
            if (entity == null)
                return "Silinecek User-Role ilişkisi bulunamadı.";

            AppUser user = await _userManager.FindByIdAsync(entity.UserId.ToString());
            if (user == null)
                return "Kullanıcı bulunamadı";

            AppRole role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return "Rol bulunamadı";

            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (!result.Succeeded)
                return string.Join("|", result.Errors.Select(e => e.Description));

            await _appUserRoleRepository.DeleteByUserAndRoleAsync(userId, roleId);

            return "Kullanıcı rolden başarıyla çıkarıldı";
        }

        public async Task<string> SoftDeleteByCompositeKeyAsync(int userId, int roleId)
        {
            AppUserRole entity = await _appUserRoleRepository.GetByCompositeKeyAsync(userId, roleId);
            if (entity == null)
                return "Silinecek User-Role ilişkisi bulunamadı.";

            AppUser user = await _userManager.FindByIdAsync(entity.UserId.ToString());
            if (user == null)
                return "Kullanıcı bulunamadı";

            AppRole role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return "Rol bulunamadı";

            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (!result.Succeeded)
                return string.Join("|", result.Errors.Select(e => e.Description));

            entity.Status = Project.Domain.Enums.DataStatus.Deleted;
            entity.DeletionDate = DateTime.Now;
            await _appUserRoleRepository.UpdateAsync(entity, entity);

            return "Kullanıcı rolden başarıyla çıkarıldı";
        }
    }
}