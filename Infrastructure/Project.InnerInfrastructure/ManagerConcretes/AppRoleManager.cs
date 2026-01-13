using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class AppRoleManager(IAppRoleRepository appRoleRepository,IMapper mapper, IValidator<AppRoleDTO> appRoleValidator,RoleManager<AppRole> roleManager):BaseManager<AppRole,AppRoleDTO>(appRoleRepository,mapper,appRoleValidator),IAppRoleManager
    {
        private readonly IValidator<AppRoleDTO> _appRoleValidator = appRoleValidator;
        private readonly IMapper _mapper = mapper;
        private readonly RoleManager<AppRole> _roleManager=roleManager;

        public override async Task<string> CreateAsync(AppRoleDTO dto)
        {
            ValidationResult validationResult = await _appRoleValidator.ValidateAsync(dto);
            if(!validationResult.IsValid)
            {
                return string.Join("|", validationResult.Errors.Select(x => x.ErrorMessage));
            }

            AppRole role=_mapper.Map<AppRole>(dto);
            role.InsertedDate = DateTime.Now;
            role.Status= Domain.Enums.DataStatus.Inserted;

            IdentityResult createResult = await _roleManager.CreateAsync(role);
            if(!createResult.Succeeded)
            {
                return string.Join ("|", createResult.Errors.Select(x => x.Description));
            }
            return "Rol başarıyla oluşturuldu.";


        }
        public override async Task<string> HardDeleteAsync(int id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());
            if(role==null)
            {
                return "Silinecek Rol bulunamadı.";
            }
            if(role.Status!= Domain.Enums.DataStatus.Deleted)
            {
                return "Sadece silinmiş roller silinebilir.";
            }

            IdentityResult deleteResult =await _roleManager.DeleteAsync(role);
            if(!deleteResult.Succeeded)
            {
                return string.Join("|", deleteResult.Errors.Select(x => x.Description));
            }
            return "Rol başarıyla silindi.";
        }
        
    }
}
