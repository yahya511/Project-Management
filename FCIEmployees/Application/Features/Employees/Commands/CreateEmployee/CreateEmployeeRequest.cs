
using Domain.Enums;

namespace Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeRequest : IRequest<Unit>
    {
        public CreateEmployeeDTO createEmployeeDTO{get;set;}


    }
}


