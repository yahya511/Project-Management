using Application.IServices;
using Application.Features.Departments.Queries.GetDepartmentById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.DTOs;
namespace FCIProjects.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmployeeGrpcService _employeeGrpcService; // إضافة gRPC service

        public DepartmentsController(IMediator mediator, IEmployeeGrpcService employeeGrpcService)
        {
            _mediator = mediator;
            _employeeGrpcService = employeeGrpcService;
        }

       // Get Department and Employee Data
        [HttpGet("GetDepartmentWithEmployee/{id}")]
        public async Task<IActionResult> GetDepartmentWithEmployeeData(int id)
        {
            
            // جلب بيانات القسم
            var department = await _mediator.Send(new GetDepartmentByIdRequest { DepartmentID = id });
            if (department == null)
            {
                return NotFound();
            }

            // جلب بيانات الموظف باستخدام ManagerID
            var employeeResponse = await _employeeGrpcService.GetEmployeeByIdAsync(department.ManagerID);
            if (employeeResponse == null)
            {
                return NotFound();
            }
            
            // إنشاء DTO لبيانات القسم والموظف
            var departmentEmployeeData = new DepartmentEmployeeDto
            {
                DepartmentID = department.DepartmentID,
                DepartmentName = department.DepartmentName,
                ManagerID = department.ManagerID,
                FirstName = employeeResponse.FirstName,
                LastName = employeeResponse.LastName,
                JobTitle = employeeResponse.JobTitle // إضافة JobTitle
            };

            return Ok(departmentEmployeeData);
        }
        // Get Department by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var result = await _mediator.Send(new GetDepartmentByIdRequest { DepartmentID = id });
            return Ok(result);
        }

        // Create Department
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // Get All Departments
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var result = await _mediator.Send(new GetAllDepartmentsRequest());
            return Ok(result);
        }

        // Update Department
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        // Delete Department
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _mediator.Send(new DeleteDepartmentRequest { DepartmentID = id });
            return NoContent();
        }
    }
}
