using Project.Application.DTOs;
using Project.Application.Enums;
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

       
        Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> expression);

        Task<OperationStatus> CreateAsync(TDto dto);
        Task<OperationStatus> UpdateAsync(TDto originalDto, TDto newDto);

        Task<OperationStatus> SoftDeleteByIdAsync(int id);
        Task<OperationStatus> HardDeleteByIdAsync(int id);
    }
}