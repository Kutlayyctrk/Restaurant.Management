using Microsoft.EntityFrameworkCore;
using Project.Contract.Repositories;
using Project.Domain.Entities.Abstract;
using Project.Persistance.ContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {

        private readonly MyContext _myContext;

        protected BaseRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public virtual async Task CreateAsync(T entity)
        {
            _myContext.Set<T>().Add(entity);
            await _myContext.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _myContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _myContext.Set<T>().FindAsync(id);
        }

        public async Task HardDeleteAsync(T entity)
        {
            _myContext.Set<T>().Remove(entity);
            await _myContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T originalEntity, T newEntity)
        {
            _myContext.Set<T>().Entry(originalEntity).CurrentValues.SetValues(newEntity);
            await _myContext.SaveChangesAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _myContext.Set<T>().Where(expression);
        }

        public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> expression)
        {
            return await _myContext.Set<T>().Where(expression).ToListAsync();
        }
    }

}
