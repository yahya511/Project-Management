


namespace Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly EmployeesDbContext _dbContext;

        public EmployeeRepository(EmployeesDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsAsync(int managerId)
        {
            return await _dbContext.Employees.AnyAsync(e => e.EmployeeID == managerId && !e.IsDeleted);
        }

    }
}
