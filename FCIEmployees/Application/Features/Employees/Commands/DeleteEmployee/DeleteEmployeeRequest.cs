
namespace Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeRequest : IRequest
    {
        public int EmployeeID { get; set; }
    }
}
