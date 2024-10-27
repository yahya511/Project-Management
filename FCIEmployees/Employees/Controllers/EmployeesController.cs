

namespace Employees.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator; // استخدام Mediator
        }

        

        // Create Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // Get Employee by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await _mediator.Send(new GetEmployeeByIdRequest { EmployeeID = id });
            return Ok(result);
        }

        // Get All Employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _mediator.Send(new GetAllEmployeesRequest());
            return Ok(result);
        }

        // Update Employee
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        // Delete Employee
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _mediator.Send(new DeleteEmployeeRequest { EmployeeID = id });
            return NoContent();
        }
    }
}
