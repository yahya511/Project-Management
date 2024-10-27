
namespace Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeRequest, Unit>
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentGrpcService _departmentGrpcService; // إضافة gRPC service

        public UpdateEmployeeHandler(IUnitOfWork unitOfWork,IDepartmentGrpcService departmentGrpcService)
        {
            _unitOfWork = unitOfWork;
            _departmentGrpcService=departmentGrpcService;
        }

        public async Task<Unit> Handle(UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            // التحقق من صحة DepartmentID وأنه موجود بالفعل في قاعدة البيانات
            var departmentExists = await _departmentGrpcService.CheckDepartmentExistsAsync(request.updateEmployee.DepartmentID);
            if (!departmentExists)
            {
                throw new KeyNotFoundException($"Department with ID {request.updateEmployee.DepartmentID} not found.");
            }
            // الحصول على الموظف بناءً على EmployeeID
            var employee = await _unitOfWork.Employees.GetEntityByIdAsync(request.EmployeeID);
            if (employee == null)
            {
                throw new Exception("Employee not found.");
            }

            
            // إزالة المسافات وجعل العملية غير حساسة لحالة الأحرف لتحويل JobTitle من نص إلى enum
            string jobTitleWithoutSpaces = request.updateEmployee.JobTitle.Replace(" ", "", StringComparison.OrdinalIgnoreCase);

            if (!Enum.TryParse(typeof(JobTitle), jobTitleWithoutSpaces, true, out var jobTitleEnum))
            {
                throw new ArgumentException($"Invalid JobTitle value: {request.updateEmployee.JobTitle}");
            }

            // التحقق من صحة ManagerID وأنه موجود بالفعل في قاعدة البيانات
            var managerExists = await _unitOfWork.Employees.GetEntityByIdAsync(request.updateEmployee.ManagerID);
            if (managerExists == null)
            {
                throw new KeyNotFoundException($"Manager with ID {request.updateEmployee.ManagerID} not found.");
            }

            // التحقق من أن AddressID يشير إلى عنوان موجود (في حالة كان AddressID مدخلاً)
            if (request.updateEmployee.AddressID.HasValue)
            {
                var addressExists = await _unitOfWork.Addresses.GetEntityByIdAsync(request.updateEmployee.AddressID.Value);
                if (addressExists == null)
                {
                    throw new KeyNotFoundException($"Address with ID {request.updateEmployee.AddressID.Value} not found.");
                }
            }

            // تحديث الخصائص بناءً على الطلب
            employee.FirstName = request.updateEmployee.FirstName;
            employee.LastName = request.updateEmployee.LastName;
            employee.MiddleName = request.updateEmployee.MiddleName;
            employee.JobTitle = (JobTitle)jobTitleEnum; // تعيين JobTitle المحول من النص
            employee.DepartmentID = request.updateEmployee.DepartmentID;
            employee.ManagerID = request.updateEmployee.ManagerID;
            employee.AddressID = request.updateEmployee.AddressID;
            employee.HireDate = request.updateEmployee.HireDate;
            employee.Salary = request.updateEmployee.Salary;

            // تحديث الموظف في قاعدة البيانات
            await _unitOfWork.Employees.UpdateAsync(employee);
            await _unitOfWork.CommitAsync(); // حفظ التغييرات في قاعدة البيانات

            return Unit.Value; // إرجاع وحدة القيمة لتشير إلى النجاح
        }

    }
}
