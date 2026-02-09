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
    public class SupplierManager(ISupplierRepository supplierRepository,IUnitOfWork unitOfWork,IMapper mapper,IValidator<SupplierDTO> supplierValidator):BaseManager<Supplier,SupplierDTO>(supplierRepository,unitOfWork,mapper,supplierValidator),ISupplierManager
    {
    }
}
