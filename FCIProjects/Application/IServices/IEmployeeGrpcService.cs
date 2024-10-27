

namespace Application.IServices
{
    public interface IEmployeeGrpcService
    {
        Task<DepartmentEmployeeDto> GetEmployeeByIdAsync(int employeeId);
        Task<bool> CheckManagerExistsAsync(int managerId);
    }
}

