using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Foodstream.Application.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> ListAsync();

        Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> AddAsync(TEntity item);

        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> items);

        Task<TEntity> UpdateAsync(TEntity item);

        Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> item);

        Task RemoveAsync(TEntity item);

        Task RemoveRangeAsync(IEnumerable<TEntity> item);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
