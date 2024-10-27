
namespace Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentRequest : IRequest<Unit>
    {
        public int DepartmentID { get; set; } // GUID
        public string DepartmentName { get; set; }
        public int ManagerID { get; set; }
    }
}
