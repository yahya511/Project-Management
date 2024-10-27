

namespace Infrastructure.IRepositories
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        Task<bool> ExistsAsync(int managerId);

    }
}