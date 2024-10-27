
namespace Infrastructure.Repositories
{
    public class TownRepository : GenericRepository<Town>, ITownRepository
    {
        public TownRepository(EmployeesDbContext dbContext) : base(dbContext)
        {
        }

        // يمكن إضافة وظائف إضافية خاصة بـ Town هنا إذا لزم الأمر.
    }
}




















//======================================
/* using Domain.Models;
using Infrastructure.DbContexts;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TownRepository : ITownRepository
    {
        private readonly EmployeesDbContext _dbContext;

        public TownRepository(EmployeesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Town> AddAsync(Town town)
        {
            _dbContext.Towns.Add(town);
            await _dbContext.SaveChangesAsync();
            return town;
        }

        public async Task<Town?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Towns.FindAsync(id);
        }

        public async Task<List<Town>> GetAllAsync()
        {
            return await _dbContext.Towns.ToListAsync();
        }

        public async Task UpdateAsync(Town town)
        {
            _dbContext.Towns.Update(town);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var town = await _dbContext.Towns.FindAsync(id);
            if (town != null)
            {
                _dbContext.Towns.Remove(town);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
 */