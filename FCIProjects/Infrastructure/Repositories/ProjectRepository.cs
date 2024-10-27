

namespace Infrastructure.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ProjectsDbContext dbContext) : base(dbContext)
        {
        }

        // يمكن إضافة وظائف إضافية خاصة بـ Project هنا إذا لزم الأمر.
    }
}
