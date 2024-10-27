
namespace Application.Features.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentRequest : IRequest
    {
        public int DepartmentID { get; set; } // GUID
        public string DepartmentName { get; set; }
        public int ManagerID { get; set; }
    }
}
