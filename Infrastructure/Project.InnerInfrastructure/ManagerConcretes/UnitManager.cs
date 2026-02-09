using AutoMapper;
using FluentValidation;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Concretes;


namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class UnitManager(IUnitRepository unitRepository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<UnitDTO> unitValidator) : BaseManager<Unit, UnitDTO>(unitRepository, unitOfWork, mapper, unitValidator), IUnitManager
    {
    }
}