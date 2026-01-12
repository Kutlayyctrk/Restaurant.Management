using Project.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contract.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<List<T>> WhereAsync(Expression<Func<T, bool>> expression);

        Task CreateAsync(T entity);
        Task UpdateAsync(T originalEntity, T newEntity);
        Task HardDeleteAsync(T entity);
    }
}
