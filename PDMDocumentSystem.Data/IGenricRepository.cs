using System.Linq.Expressions;

namespace PDMDocumentSystem;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity?>> GetAllAsync();
    Task<IEnumerable<TEntity?>> GetByConditionAsync(Expression<Func<TEntity?, bool>> predicate);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task CreateAsync(TEntity? entity);
    Task UpdateAsync(TEntity? entity);
    Task DeleteAsync(TEntity? entity);
}