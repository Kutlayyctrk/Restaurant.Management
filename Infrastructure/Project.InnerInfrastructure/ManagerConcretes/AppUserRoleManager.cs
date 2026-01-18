using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Project.Application.DTOs;
using Project.Application.Enums;  
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class AppUserRoleManager : BaseManager<AppUserRole, AppUserRoleDTO>, IAppUserRoleManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAppUserRoleRepository _appUserRoleRepository;
        private readonly IMapper _mapper;

        public AppUserRoleManager(
            IAppUserRoleRepository appUserRoleRepository,
            IMapper mapper,
            IValidator<AppUserRoleDTO> appUserRoleValidator,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager
        ) : base(appUserRoleRepository, mapper, appUserRoleValidator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appUserRoleRepository = appUserRoleRepository;
            _mapper = mapper;
        }

        public override async Task<OperationStatus> CreateAsync(AppUserRoleDTO dto)
        {
            AppUser user = await _userManager.FindByIdAsync(dto.UserId.ToString());
            if (user == null) return OperationStatus.NotFound;

            AppRole role = await _roleManager.FindByIdAsync(dto.RoleId.ToString());
            if (role == null) return OperationStatus.NotFound;

            AppUserRole existing = await _appUserRoleRepository.GetByCompositeKeyAsync(dto.UserId, dto.RoleId);
            if (existing != null) return OperationStatus.AlreadyExists;

            IdentityResult result = await _userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded) return OperationStatus.Failed;

            AppUserRole domainEntity = _mapper.Map<AppUserRole>(dto);
            domainEntity.InsertedDate = DateTime.Now;
            domainEntity.Status = Project.Domain.Enums.DataStatus.Inserted;

            AppUserRole trackedEntity = _appUserRoleRepository.GetLocalTrackedEntity(dto.UserId, dto.RoleId);
            if (trackedEntity == null)
            {
                await _appUserRoleRepository.CreateAsync(domainEntity);
            }

            return OperationStatus.Success;
        }

        public async Task<OperationStatus> HardDeleteByCompositeKeyAsync(int userId, int roleId)
        {
            AppUserRole entity = await _appUserRoleRepository.GetByCompositeKeyAsync(userId, roleId);
            if (entity == null) return OperationStatus.NotFound;

            AppUser user = await _userManager.FindByIdAsync(entity.UserId.ToString());
            if (user == null) return OperationStatus.NotFound;

            AppRole role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return OperationStatus.NotFound;

            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (!result.Succeeded) return OperationStatus.Failed;

            await _appUserRoleRepository.DeleteByUserAndRoleAsync(userId, roleId);

            return OperationStatus.Success;
        }

        public async Task<OperationStatus> SoftDeleteByCompositeKeyAsync(int userId, int roleId)
        {
            AppUserRole entity = await _appUserRoleRepository.GetByCompositeKeyAsync(userId, roleId);
            if (entity == null) return OperationStatus.NotFound;

            AppUser user = await _userManager.FindByIdAsync(entity.UserId.ToString());
            if (user == null) return OperationStatus.NotFound;

            AppRole role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return OperationStatus.NotFound;

            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (!result.Succeeded) return OperationStatus.Failed;

            entity.Status = Project.Domain.Enums.DataStatus.Deleted;
            entity.DeletionDate = DateTime.Now;
            await _appUserRoleRepository.UpdateAsync(entity, entity);

            return OperationStatus.Success;
        }
    }
}