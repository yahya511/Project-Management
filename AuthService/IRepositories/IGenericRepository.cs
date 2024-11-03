using System.Linq.Expressions;

namespace AuthService.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(object id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task SaveAsync();
    }
}
