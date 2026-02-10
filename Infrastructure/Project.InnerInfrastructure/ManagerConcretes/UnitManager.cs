using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;


namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class UnitManager(IUnitRepository unitRepository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<UnitDTO> unitValidator, ILogger<UnitManager> logger) : BaseManager<Unit, UnitDTO>(unitRepository, unitOfWork, mapper, unitValidator, logger), IUnitManager
    {
    }
}