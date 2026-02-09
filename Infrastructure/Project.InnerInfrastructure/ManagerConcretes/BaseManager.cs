using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;
using Project.Contract.Repositories;
using Project.Domain.Entities.Abstract;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.InnerInfrastructure.ManagerConcretes
{
    public class BaseManager<TEntity, TDto> : IManager<TEntity, TDto>
        where TEntity : class, IEntity
        where TDto : BaseDto
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<TDto> _validator;

        public BaseManager(IRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<TDto> validator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public virtual async Task<OperationStatus> CreateAsync(TDto dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return OperationStatus.ValidationError;

            TEntity domainEntity = _mapper.Map<TEntity>(dto);
            domainEntity.InsertedDate = DateTime.Now;
            domainEntity.Status = DataStatus.Inserted;

            await _repository.CreateAsync(domainEntity);
            await _unitOfWork.CommitAsync();

            return OperationStatus.Success;
        }

        public async Task<List<TDto>> GetActives()
        {
            List<TEntity> activeEntities = await _repository.WhereAsync(x => x.Status != DataStatus.Deleted);
            return _mapper.Map<List<TDto>>(activeEntities);
        }

        public virtual async Task<List<TDto>> GetAllAsync()
        {
            List<TEntity> allEntities = await _repository.GetAllAsync();
            return _mapper.Map<List<TDto>>(allEntities);
        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            TEntity entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<List<TDto>> GetPassives()
        {
            List<TEntity> passiveEntities = await _repository.WhereAsync(x => x.Status == DataStatus.Deleted);
            return _mapper.Map<List<TDto>>(passiveEntities);
        }

        public virtual async Task<OperationStatus> HardDeleteByIdAsync(int id)
        {
            TEntity domainEntity = await _repository.GetByIdAsync(id);
            if (domainEntity == null)
                return OperationStatus.NotFound;

            if (domainEntity.Status != DataStatus.Deleted)
                return OperationStatus.Failed;

            await _repository.HardDeleteAsync(domainEntity);
            await _unitOfWork.CommitAsync();

            return OperationStatus.Success;
        }

        public virtual async Task<OperationStatus> SoftDeleteByIdAsync(int id)
        {
            TEntity originalEntity = await _repository.GetByIdAsync(id);
            if (originalEntity == null)
                return OperationStatus.NotFound;

            originalEntity.Status = DataStatus.Deleted;
            originalEntity.DeletionDate = DateTime.Now;

            await _repository.UpdateAsync(originalEntity);
            await _unitOfWork.CommitAsync();

            return OperationStatus.Success;
        }

        public virtual async Task<OperationStatus> UpdateAsync(TDto originalDto, TDto newDto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(newDto);
            if (!validationResult.IsValid)
                return OperationStatus.ValidationError;

            TEntity originalEntity = await _repository.GetByIdAsync(originalDto.Id);
            if (originalEntity == null)
                return OperationStatus.NotFound;

            _mapper.Map(newDto, originalEntity);

            originalEntity.Status = DataStatus.Updated;
            originalEntity.UpdatedDate = DateTime.Now;

            await _repository.UpdateAsync(originalEntity);
            await _unitOfWork.CommitAsync();

            return OperationStatus.Success;
        }

        public async Task<List<TDto>> WhereAsync(Expression<Func<TEntity, bool>> expression)
        {
            List<TEntity> entities = await _repository.WhereAsync(expression);
            return _mapper.Map<List<TDto>>(entities);
        }
    }
}