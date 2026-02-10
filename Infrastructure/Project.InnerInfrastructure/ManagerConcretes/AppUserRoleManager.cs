using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;
using Project.Application.Results;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using System;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class AppUserRoleManager : BaseManager<AppUserRole, AppUserRoleDTO>, IAppUserRoleManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAppUserRoleRepository _appUserRoleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AppUserRoleManager(
            IAppUserRoleRepository appUserRoleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<AppUserRoleDTO> appUserRoleValidator,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            ILogger<AppUserRoleManager> logger
        ) : base(appUserRoleRepository, unitOfWork, mapper, appUserRoleValidator, logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appUserRoleRepository = appUserRoleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public override async Task<Result> CreateAsync(AppUserRoleDTO dto)
        {
            AppUser user = await _userManager.FindByIdAsync(dto.UserId.ToString());
            if (user == null) return Result.Failure(OperationStatus.NotFound, "Kullanıcı bulunamadı.");

            AppRole role = await _roleManager.FindByIdAsync(dto.RoleId.ToString());
            if (role == null) return Result.Failure(OperationStatus.NotFound, "Rol bulunamadı.");

            AppUserRole existing = await _appUserRoleRepository.GetByCompositeKeyAsync(dto.UserId, dto.RoleId);
            if (existing != null) return Result.Failure(OperationStatus.AlreadyExists, "Bu rol zaten atanmış.");

            IdentityResult result = await _userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded) return Result.Failure(OperationStatus.Failed, "Rol atanamadı.");

            AppUserRole domainEntity = _mapper.Map<AppUserRole>(dto);
            domainEntity.InsertedDate = DateTime.Now;
            domainEntity.Status = Project.Domain.Enums.DataStatus.Inserted;

            AppUserRole trackedEntity = _appUserRoleRepository.GetLocalTrackedEntity(dto.UserId, dto.RoleId);
            if (trackedEntity == null)
            {
                await _appUserRoleRepository.CreateAsync(domainEntity);
            }

            await _unitOfWork.CommitAsync();

            return Result.Succeed("Rol atandı.");
        }

        public async Task<AppUserRoleDTO> GetByCompositeKeyAsync(int userId, int roleId)
        {
            AppUserRole entity = await _appUserRoleRepository.GetByCompositeKeyAsync(userId, roleId);
            return _mapper.Map<AppUserRoleDTO>(entity);
        }

        public async Task<IList<int>> GetRoleIdsByUserIdAsync(int userId)
        {
            return await _appUserRoleRepository.GetRoleIdsByUserIdAsync(userId);
        }

        public async Task<Result> HardDeleteByCompositeKeyAsync(int userId, int roleId)
        {
            AppUserRole entity = await _appUserRoleRepository.GetByCompositeKeyAsync(userId, roleId);
            if (entity == null) return Result.Failure(OperationStatus.NotFound, "Kayıt bulunamadı.");

            AppUser user = await _userManager.FindByIdAsync(entity.UserId.ToString());
            if (user == null) return Result.Failure(OperationStatus.NotFound, "Kullanıcı bulunamadı.");

            AppRole role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return Result.Failure(OperationStatus.NotFound, "Rol bulunamadı.");

            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (!result.Succeeded) return Result.Failure(OperationStatus.Failed, "Rol kaldırılamadı.");

            await _appUserRoleRepository.DeleteByUserAndRoleAsync(userId, roleId);

            return Result.Succeed("Rol kaldırıldı.");
        }

        public async Task<Result> SoftDeleteByCompositeKeyAsync(int userId, int roleId)
        {
            AppUserRole entity = await _appUserRoleRepository.GetByCompositeKeyAsync(userId, roleId);
            if (entity == null) return Result.Failure(OperationStatus.NotFound, "Kayıt bulunamadı.");

            AppUser user = await _userManager.FindByIdAsync(entity.UserId.ToString());
            if (user == null) return Result.Failure(OperationStatus.NotFound, "Kullanıcı bulunamadı.");

            AppRole role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return Result.Failure(OperationStatus.NotFound, "Rol bulunamadı.");

            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (!result.Succeeded) return Result.Failure(OperationStatus.Failed, "Rol kaldırılamadı.");

            entity.Status = Project.Domain.Enums.DataStatus.Deleted;
            entity.DeletionDate = DateTime.Now;

            await _appUserRoleRepository.UpdateByCompositeKeyAsync(entity);
            await _unitOfWork.CommitAsync();

            return Result.Succeed("Rol kaldırıldı.");
        }

        public async Task<Result> UpdateByCompositeKeyAsync(AppUserRoleDTO dto)
        {
            AppUserRole entity = await _appUserRoleRepository.GetByCompositeKeyAsync(dto.UserId, dto.RoleId);
            if (entity == null) return Result.Failure(OperationStatus.NotFound, "Kayıt bulunamadı.");

            _mapper.Map(dto, entity);
            entity.Status = Project.Domain.Enums.DataStatus.Updated;
            entity.UpdatedDate = DateTime.Now;

            await _appUserRoleRepository.UpdateByCompositeKeyAsync(entity);
            await _unitOfWork.CommitAsync();

            return Result.Succeed("Güncellendi.");
        }
    }
}