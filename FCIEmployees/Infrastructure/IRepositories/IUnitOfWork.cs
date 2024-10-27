namespace Infrastructure.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository Addresses { get; }
        ITownRepository Towns { get; }
        IEmployeeRepository Employees { get;  }
        Task<int> CommitAsync(); // لحفظ جميع التغييرات في جلسة واحدة
    }
}
