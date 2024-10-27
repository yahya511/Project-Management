

using System.Linq.Expressions;

namespace Infrastructure.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Func<T, bool> predicate);
        Task<T> GetEntityByIdAsync(int id); // normal
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> where);

    }
}
