namespace Infrastructure.Repositories
{
    public class EmployeeProjectRepository : GenericRepository<EmployeesProjects>, IEmployeeProjectRepository
    {
        public EmployeeProjectRepository(ProjectsDbContext dbContext) : base(dbContext)
        {
        }

        // يمكن إضافة وظائف إضافية خاصة بـ EmployeesProjects هنا إذا لزم الأمر.
    }
}
