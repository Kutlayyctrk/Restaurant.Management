using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Project.Application.DTOs;
using Project.Application.Enums;   
using Project.Application.Managers;
using Project.Application.Results;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using System.Linq;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class AppRoleManager : BaseManager<AppRole, AppRoleDTO>, IAppRoleManager
    {
        private readonly IValidator<AppRoleDTO> _appRoleValidator;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public AppRoleManager(
            IAppRoleRepository appRoleRepository,
            IMapper mapper,
            IValidator<AppRoleDTO> appRoleValidator,
            RoleManager<AppRole> roleManager,
            IUnitOfWork unitOfWork,
            ILogger<AppRoleManager> logger
        ) : base(appRoleRepository, unitOfWork, mapper, appRoleValidator, logger)
        {
            _appRoleValidator = appRoleValidator;
            _mapper = mapper;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public override async Task<Result> CreateAsync(AppRoleDTO dto)
        {
            ValidationResult validationResult = await _appRoleValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result.Failure(OperationStatus.ValidationError, errors);
            }

            AppRole role = _mapper.Map<AppRole>(dto);
            role.InsertedDate = DateTime.Now;
            role.Status = Project.Domain.Enums.DataStatus.Inserted;

            IdentityResult createResult = await _roleManager.CreateAsync(role);
            if (!createResult.Succeeded)
            {
                return Result.Failure(OperationStatus.Failed, "Rol oluşturulamadı.");
            }

            return Result.Succeed("Rol oluşturuldu.");
        }

        public override async Task<Result> HardDeleteByIdAsync(int id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return Result.Failure(OperationStatus.NotFound, "Rol bulunamadı.");
            }

            if (role.Status != Domain.Enums.DataStatus.Deleted)
            {
                return Result.Failure(OperationStatus.Failed, "Önce soft delete yapılmalıdır.");
            }

            IdentityResult deleteResult = await _roleManager.DeleteAsync(role);
            if (!deleteResult.Succeeded)
            {
                return Result.Failure(OperationStatus.Failed, "Rol silinemedi.");
            }

            return Result.Succeed("Rol silindi.");
        }
    }
}