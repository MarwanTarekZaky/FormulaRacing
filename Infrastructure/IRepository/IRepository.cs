using System.Linq.Expressions;

namespace Infrastructure.IRepository;
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int? id, params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[]? includes);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities); 
    Task AddRangeAsync(IEnumerable<T> entities);

    Task<T?> GetLastAsync(
        Expression<Func<T, bool>>? predicate = null, 
        Expression<Func<T, object>>? orderBy = null,
        params Expression<Func<T, object>>[]? includes);
}