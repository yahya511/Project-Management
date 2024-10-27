using Grpc.Core;
using Proto;

namespace Application.Services
{
    public class EmployeeService : Proto.EmployeeService.EmployeeServiceBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public override async Task<GetEmployeeByIdResponse> GetEmployeeById(GetEmployeeByIdRequest request, ServerCallContext context)
        {
            try
            {
                var employee = await _employeeRepository.GetEntityByIdAsync(request.EmployeeId);

                if (employee == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "Employee not found"));
                }

                // قم بتحويل رقم JobTitle إلى النص المقابل له باستخدام الـ enum
                string jobTitleText = Enum.GetName(typeof(JobTitle), employee.JobTitle);

                return new GetEmployeeByIdResponse
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    JobTitle = (int)employee.JobTitle // إرسال الرقم كـ int
                };

            }
            catch (Exception ex)
            {
                // يمكنك تسجيل الخطأ هنا باستخدام Logger إذا كان متوفرًا
                throw new RpcException(new Status(StatusCode.Unknown, $"An error occurred: {ex.Message}"));
            }
        }

        public override async Task<EmployeeExistResponse> EmployeeExist(EmployeeExistRequest request, ServerCallContext context)
        {
            var exists = await _employeeRepository.ExistsAsync(request.ManagerId);
            return new EmployeeExistResponse { Exists = exists };
        }

    }
}
