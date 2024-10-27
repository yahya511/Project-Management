

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ProjectsDbContext _dbContext;

        public DepartmentRepository(ProjectsDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        

        public async Task<bool> ExistsAsync(int departmentId)
        {
            return await _dbContext.Departments.AnyAsync(e => e.DepartmentID == departmentId && !e.IsDeleted);
        }

        public async Task<bool> ManagerExistsAsync(int managerId)
        {
            return await _dbContext.Departments.AnyAsync(e => e.ManagerID == managerId );
        }

    }
}
