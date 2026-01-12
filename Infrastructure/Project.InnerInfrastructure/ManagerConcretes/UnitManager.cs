using AutoMapper;
using FluentValidation;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class UnitManager(IUnitRepository unitRepository,IMapper mapper,IValidator<UnitDTO> unitValidator):BaseManager<Unit,UnitDTO>(unitRepository,mapper,unitValidator),IUnitManager
    {
    }
}
