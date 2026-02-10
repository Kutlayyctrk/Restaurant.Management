using Project.Application.DTOs;
using Project.Application.Enums;
using Project.Application.Results;
using Project.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Application.Managers
{
    public interface IManager<TEntity, TDto>
        where TEntity : class, IEntity
        where TDto : BaseDto
    {
        Task<List<TDto>> GetAllAsync();
        Task<List<TDto>> GetActives();
        Task<List<TDto>> GetPassives();
        Task<TDto> GetByIdAsync(int id);
        Task<PagedResult<TDto>> GetPagedAsync(int page, int pageSize);

        Task<List<TDto>> WhereAsync(Expression<Func<TEntity, bool>> expression);

        Task<Result> CreateAsync(TDto dto);
        Task<Result> UpdateAsync(TDto originalDto, TDto newDto);

        Task<Result> SoftDeleteByIdAsync(int id);
        Task<Result> HardDeleteByIdAsync(int id);
    }
}