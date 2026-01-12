using AutoMapper;
using FluentValidation;
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
    public class AppUserManager(IAppUserRepository appUserRepository,IMapper mapper,IValidator<AppUserDTO>appUserValidator):BaseManager<AppUser,AppUserDTO>(appUserRepository,mapper,appUserValidator), IAppUserManager
    {
    }
}
