using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Project.Application.DTOs;
using Project.Application.Enums;   
using Project.Application.Managers;
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
            IUnitOfWork unitOfWork
        ) : base(appRoleRepository, unitOfWork, mapper, appRoleValidator)
        {
            _appRoleValidator = appRoleValidator;
            _mapper = mapper;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public override async Task<OperationStatus> CreateAsync(AppRoleDTO dto)
        {
            ValidationResult validationResult = await _appRoleValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return OperationStatus.ValidationError;
            }

            AppRole role = _mapper.Map<AppRole>(dto);
            role.InsertedDate = DateTime.Now;
            role.Status = Project.Domain.Enums.DataStatus.Inserted;

            IdentityResult createResult = await _roleManager.CreateAsync(role);
            if (!createResult.Succeeded)
            {
                return OperationStatus.Failed;
            }

            return OperationStatus.Success;
        }

        public override async Task<OperationStatus> HardDeleteByIdAsync(int id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return OperationStatus.NotFound;
            }

            if (role.Status != Domain.Enums.DataStatus.Deleted)
            {
                return OperationStatus.Failed;
            }

            IdentityResult deleteResult = await _roleManager.DeleteAsync(role);
            if (!deleteResult.Succeeded)
            {
                return OperationStatus.Failed;
            }

            return OperationStatus.Success;
        }
    }
}