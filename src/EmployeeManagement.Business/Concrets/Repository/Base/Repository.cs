using EmployeeManagement.Data.Base;
using EmployeeManagement.DataAccess.Context;
using EmployeeManagement.Repository.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagement.Repository.Implementations.Base;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> 
    where TEntity : BaseEntity<TKey>
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<TEntity?> GetAsync(TKey id,params string[] includes)
    {
        IQueryable<TEntity> query = GetQuery(includes);
        return await query.FirstOrDefaultAsync(e=> Equals(id,e.Id));
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, params string[] includes)
    {
        IQueryable<TEntity> query = GetQuery(includes);
        return await query.Where(expression).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression, params string[] includes)
    {
        IQueryable<TEntity> query = GetQuery(includes);
        return expression is null
            ? await query.ToListAsync()
            : await query.Where(expression).ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        //_dbContext.Entry(entity).State = EntityState.Added;
    }

    public void Update(TEntity entity)
    {
         _dbContext.Set<TEntity>().Update(entity); 
    }

    public void Remove(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }
    private IQueryable<TEntity> GetQuery(params string[] includes)
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
        foreach (string include in includes)
        {
            query = query.Include(include);
        }
        return query;
    }
    
}
