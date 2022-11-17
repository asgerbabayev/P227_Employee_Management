using EmployeeManagement.Data.Base;
using System.Linq.Expressions;

namespace EmployeeManagement.Repository.Interfaces.Base;

public interface IRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
{
    Task<TEntity?> GetAsync(TKey id, params string[] includes);

    Task<TEntity?> GetAsync(Expression<Func<TEntity,bool>> expression,params string[] includes);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression, params string[] includes);

    Task AddAsync(TEntity entity);

    void Update(TEntity entity);
    void Remove(TEntity entity);
}
