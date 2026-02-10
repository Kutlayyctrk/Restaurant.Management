using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Managers;
using Project.Application.Results;
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
        protected readonly ILogger _logger;

        public BaseManager(IRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<TDto> validator, ILogger logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public virtual async Task<Result> CreateAsync(TDto dto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                _logger.LogWarning("Validation failed for {EntityType}: {Errors}", typeof(TEntity).Name, string.Join(", ", errors));
                return Result.Failure(OperationStatus.ValidationError, errors);
            }

            TEntity domainEntity = _mapper.Map<TEntity>(dto);
            domainEntity.InsertedDate = DateTime.Now;
            domainEntity.Status = DataStatus.Inserted;

            await _repository.CreateAsync(domainEntity);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("{EntityType} created successfully.", typeof(TEntity).Name);
            return Result.Succeed("Kayıt başarıyla oluşturuldu.");
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

        public async Task<PagedResult<TDto>> GetPagedAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            (List<TEntity> items, int totalCount) = await _repository.GetPagedAsync(page, pageSize, x => x.Status != DataStatus.Deleted);

            return new PagedResult<TDto>
            {
                Items = _mapper.Map<List<TDto>>(items),
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<List<TDto>> GetPassives()
        {
            List<TEntity> passiveEntities = await _repository.WhereAsync(x => x.Status == DataStatus.Deleted);
            return _mapper.Map<List<TDto>>(passiveEntities);
        }

        public virtual async Task<Result> HardDeleteByIdAsync(int id)
        {
            TEntity domainEntity = await _repository.GetByIdAsync(id);
            if (domainEntity == null)
            {
                _logger.LogWarning("{EntityType} with Id {Id} not found for hard delete.", typeof(TEntity).Name, id);
                return Result.Failure(OperationStatus.NotFound, "Kayıt bulunamadı.");
            }

            if (domainEntity.Status != DataStatus.Deleted)
            {
                _logger.LogWarning("{EntityType} with Id {Id} is not soft-deleted, cannot hard delete.", typeof(TEntity).Name, id);
                return Result.Failure(OperationStatus.Failed, "Önce soft delete yapılmalıdır.");
            }

            await _repository.HardDeleteAsync(domainEntity);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("{EntityType} with Id {Id} hard deleted.", typeof(TEntity).Name, id);
            return Result.Succeed("Kayıt kalıcı olarak silindi.");
        }

        public virtual async Task<Result> SoftDeleteByIdAsync(int id)
        {
            TEntity originalEntity = await _repository.GetByIdAsync(id);
            if (originalEntity == null)
            {
                _logger.LogWarning("{EntityType} with Id {Id} not found for soft delete.", typeof(TEntity).Name, id);
                return Result.Failure(OperationStatus.NotFound, "Kayıt bulunamadı.");
            }

            originalEntity.Status = DataStatus.Deleted;
            originalEntity.DeletionDate = DateTime.Now;

            await _repository.UpdateAsync(originalEntity);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("{EntityType} with Id {Id} soft deleted.", typeof(TEntity).Name, id);
            return Result.Succeed("Kayıt silindi.");
        }

        public virtual async Task<Result> UpdateAsync(TDto originalDto, TDto newDto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(newDto);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                _logger.LogWarning("Validation failed for {EntityType} update: {Errors}", typeof(TEntity).Name, string.Join(", ", errors));
                return Result.Failure(OperationStatus.ValidationError, errors);
            }

            TEntity originalEntity = await _repository.GetByIdAsync(originalDto.Id);
            if (originalEntity == null)
            {
                _logger.LogWarning("{EntityType} with Id {Id} not found for update.", typeof(TEntity).Name, originalDto.Id);
                return Result.Failure(OperationStatus.NotFound, "Kayıt bulunamadı.");
            }

            _mapper.Map(newDto, originalEntity);

            originalEntity.Status = DataStatus.Updated;
            originalEntity.UpdatedDate = DateTime.Now;

            await _repository.UpdateAsync(originalEntity);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("{EntityType} with Id {Id} updated successfully.", typeof(TEntity).Name, originalDto.Id);
            return Result.Succeed("Kayıt başarıyla güncellendi.");
        }

        public async Task<List<TDto>> WhereAsync(Expression<Func<TEntity, bool>> expression)
        {
            List<TEntity> entities = await _repository.WhereAsync(expression);
            return _mapper.Map<List<TDto>>(entities);
        }
    }
}