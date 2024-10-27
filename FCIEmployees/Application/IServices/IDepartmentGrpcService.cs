namespace Application.IServices
{
    public interface IDepartmentGrpcService
{
    Task<bool> CheckDepartmentExistsAsync(int departmentId);
    Task<bool> CheckManagerExistsAsync(int managerId);
}
}
