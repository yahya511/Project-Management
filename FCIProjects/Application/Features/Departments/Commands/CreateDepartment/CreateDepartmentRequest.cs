
namespace Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentRequest : IRequest<Unit>
    {
        public string DepartmentName { get; set; }
        public int ManagerID { get; set; }
    }
}


