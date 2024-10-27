namespace Infrastructure.IRepositories
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<bool> ExistsAsync(int departmentId);
        Task<bool> ManagerExistsAsync(int managerId );
    }
}