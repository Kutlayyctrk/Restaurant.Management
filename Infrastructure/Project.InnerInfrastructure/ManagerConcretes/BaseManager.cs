using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Project.Application.DTOs;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class BaseManager<TEntity, TDto> : IManager<TEntity, TDto> where TEntity: class,IEntity where TDto : BaseDto
    {
        private readonly IRepository<TEntity> repository;
        private readonly IMapper _mapper;
        private readonly IValidator<TDto> _validator;

        public BaseManager(IRepository<TEntity> repository, IMapper mapper, IValidator<TDto> validator)
        {
            this.repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public Task<string> CreateAsync(TDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<List<TDto>> GetActives()
        {
            throw new NotImplementedException();
        }

        public Task<List<TDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TDto>> GetPassives()
        {
            throw new NotImplementedException();
        }

        public Task<string> HardDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(int id, TDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<List<TDto>> Where(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
