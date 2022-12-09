using Foodstream.Infrastructure.Postgre;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Foodstream.Application.Interfaces;

namespace Foodstream.Infrastructure.Postgre;
 
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    protected PostgreContext Context { get; }

    public GenericRepository(PostgreContext context)
    {
        Context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> GetAsync(int id) => await _dbSet.FindAsync(id);

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.FirstOrDefaultAsync(predicate);

    public async Task<IEnumerable<TEntity>> ListAsync() => await _dbSet.ToListAsync();

    public async Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();

    public async Task<TEntity> AddAsync(TEntity item)
    {
        await _dbSet.AddAsync(item);
        await Context.SaveChangesAsync();
        return item;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> items)
    {
        await _dbSet.AddRangeAsync(items);
        await Context.SaveChangesAsync();
        return items;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity item)
    {
        Context.Entry(item).State = EntityState.Modified;
        await Context.SaveChangesAsync();
        return item;
    }

    public virtual async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> items)
    {
        foreach (var item in items)
            Context.Entry(item).State = EntityState.Modified;

        await Context.SaveChangesAsync();
        return items;
    }

    public async Task RemoveAsync(TEntity item)
    {
        _dbSet.Remove(item);
        await Context.SaveChangesAsync();
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> items)
    {
        _dbSet.RemoveRange(items);
        await Context.SaveChangesAsync();
    }

    public async Task<int> CountAsync() => await _dbSet.CountAsync();

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.CountAsync(predicate);
}
