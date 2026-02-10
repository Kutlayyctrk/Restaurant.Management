using Project.Domain.Entities.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Project.Contract.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<List<T>> WhereAsync(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task HardDeleteAsync(T entity);
        IQueryable<T> GetQuery();
        Task<(List<T> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, Expression<Func<T, bool>>? filter = null);
    }
}