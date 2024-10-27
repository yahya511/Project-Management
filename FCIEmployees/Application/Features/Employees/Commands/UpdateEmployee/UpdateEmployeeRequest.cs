

namespace Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeRequest : IRequest<Unit>
    {
        public int EmployeeID { get; set; }
        public CreateEmployeeDTO updateEmployee{get; set;}
        
    }
}
