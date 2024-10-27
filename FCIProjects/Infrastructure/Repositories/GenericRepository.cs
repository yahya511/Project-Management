


using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ProjectsDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ProjectsDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // return await _dbSet.ToListAsync();
            // افتراض أن الكيان يحتوي على خاصية IsDeleted 

            return await _dbSet.Where(entity => EF.Property<bool>(entity, "IsDeleted") == false).ToListAsync();
            
        }

        public async Task<IEnumerable<T>> GetAllAsync(Func<T, bool> predicate)
        {
            return await Task.FromResult(_dbSet.Where(predicate).ToList());// normal

        }

         // normal --> GetByIdAsync(int id)
        public async Task<T> GetEntityByIdAsync(int id)
        {
            // استخدام Reflection للبحث عن IsDeleted
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return null;

            // التحقق من وجود خاصية IsDeleted (بافتراض أنها من نوع bool)
            var isDeletedProperty = entity.GetType().GetProperty("IsDeleted");
            if (isDeletedProperty != null)
            {
                var isDeleted = (bool)isDeletedProperty.GetValue(entity);
                if (isDeleted)
                {
                    return null; // إرجاع null إذا كانت الكيان محذوفاً
                }
            }

            return entity;
        } 

        public async Task<T>  GetByIdAsync(Expression<Func<T, bool>> where)
        {
            // افتراض أن الكيان يحتوي على خاصية IsDeleted

            return await _dbContext.Set<T>().Where(a => a.IsDeleted==false).AsNoTracking().FirstOrDefaultAsync(where);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync(); // استخدم SaveChangesAsync المعدلة
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync(); // استخدم SaveChangesAsync المعدلة
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync(); // استخدم SaveChangesAsync المعدلة
            }
        }
    }
}
