

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectsDbContext _dbContext;


        public IProjectRepository Projects { get; private set; }
        public IDepartmentRepository Departments { get; private set; }
        
        public IEmployeeProjectRepository employeeProject { get; private set; }

        public UnitOfWork(ProjectsDbContext dbContext, 
                          IProjectRepository projectRepository,
                          IDepartmentRepository departmentRepository
                          ,IEmployeeProjectRepository employeeProjectRepository
                          )
        {
            _dbContext = dbContext;
            Projects = projectRepository;
            Departments=departmentRepository;
            employeeProject=employeeProjectRepository;
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync(); // حفظ جميع التغييرات
        }

        public void Dispose()
        {
            _dbContext.Dispose(); // تحرير الموارد
        }
    }
}
