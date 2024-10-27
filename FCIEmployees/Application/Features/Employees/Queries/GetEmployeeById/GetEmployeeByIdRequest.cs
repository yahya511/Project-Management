

namespace Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdRequest : IRequest<GetEmployeeDTO>
    {
        public int EmployeeID { get; set; }
    }
}
