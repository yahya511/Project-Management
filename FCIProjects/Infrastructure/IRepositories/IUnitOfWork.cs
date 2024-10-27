namespace Infrastructure.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {//
        IProjectRepository Projects { get; }
        IDepartmentRepository Departments { get; }
        IEmployeeProjectRepository employeeProject { get; }
        Task<int> CommitAsync(); // لحفظ جميع التغييرات في جلسة واحدة
    }
}
