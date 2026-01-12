using AutoMapper;
using FluentValidation;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Abstract;
using Project.Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class ProductManager(IProductRepository productRepository,IMapper mapper,IValidator<ProductDTO>productValidator):BaseManager<Product,ProductDTO>(productRepository,mapper,productValidator),IProductManager
    {
    }
}
