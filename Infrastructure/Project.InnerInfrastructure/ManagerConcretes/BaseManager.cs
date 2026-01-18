using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Project.Application.DTOs;
using Project.Application.Enums;
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
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<TDto> _validator;

        public BaseManager(IRepository<TEntity> repository, IMapper mapper, IValidator<TDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public virtual async Task<OperationStatus> CreateAsync(TDto dto)
        {
           ValidationResult validationResult= await _validator.ValidateAsync(dto);
            if(!validationResult.IsValid)
            {
                return OperationStatus.ValidationError;

            }

            TEntity domainEntity= _mapper.Map<TEntity>(dto);
            domainEntity.InsertedDate = DateTime.Now;
            domainEntity.Status = Domain.Enums.DataStatus.Inserted;
            await _repository.CreateAsync(domainEntity);
            return OperationStatus.Success;

        }

        public Task<List<TDto>> GetActives()
        {
           List<TEntity> activeEntities=_repository.Where(x=>x.Status!=Domain.Enums.DataStatus.Deleted).ToList();
            return Task.FromResult(_mapper.Map<List<TDto>>(activeEntities));
        }

        public virtual async Task<List<TDto>> GetAllAsync()
        {
            List<TEntity> allEntites = await _repository.GetAllAsync();
            return _mapper.Map<List<TDto>>(allEntites);
        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
           TEntity entity= await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public Task<List<TDto>> GetPassives()
        {
            List<TEntity> passiveEntities = _repository.Where(x => x.Status == Domain.Enums.DataStatus.Deleted).ToList();
            return Task.FromResult(_mapper.Map<List<TDto>>(passiveEntities));
        }
        public virtual Task<string> HardDeleteAsync(int userId, int roleId)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<OperationStatus> HardDeleteByIdAsync(int id)
        {
            TEntity domainEntity = await _repository.GetByIdAsync(id);
            if(domainEntity ==null)
            {
                return OperationStatus.NotFound;
            }
            if(domainEntity.Status!= Domain.Enums.DataStatus.Deleted)
            {
                return OperationStatus.Failed;
            }
            await _repository.HardDeleteAsync(domainEntity);
            return OperationStatus.Success;
        }

        public  virtual async Task<OperationStatus> SoftDeleteByIdAsync(int id)
        {
            TEntity originalEntity= await _repository.GetByIdAsync(id);
            if(originalEntity==null)
            {
                return OperationStatus.NotFound;
            }
            originalEntity.Status = Domain.Enums.DataStatus.Deleted;
            originalEntity.DeletionDate = DateTime.Now;
            await _repository.UpdateAsync(originalEntity, originalEntity);
            return OperationStatus.Success;
        }

        public virtual async Task<OperationStatus> UpdateAsync(TDto originalDto, TDto newDto)
        {
          
            ValidationResult validationResult = await _validator.ValidateAsync(newDto);
            if (!validationResult.IsValid)
            {
                return OperationStatus.ValidationError;
            }

            
            TEntity originalEntity = await _repository.GetByIdAsync(originalDto.Id);
            if (originalEntity == null)
            {
                return OperationStatus.NotFound;
            }

           
            _mapper.Map(newDto, originalEntity);

          
            originalEntity.Status = Domain.Enums.DataStatus.Updated;
            originalEntity.UpdatedDate = DateTime.Now;

          
            await _repository.UpdateAsync(originalEntity, originalEntity);

            return OperationStatus.Success;
        }



        public Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> expression)
        {
           List<TEntity> entities = _repository.Where(expression).ToList();
            return Task.FromResult(_mapper.Map<List<TEntity>>(entities));
        }
    }
}
