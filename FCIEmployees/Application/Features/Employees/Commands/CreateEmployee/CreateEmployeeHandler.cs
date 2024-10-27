


namespace Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentGrpcService _departmentGrpcService; // إضافة gRPC service

        public CreateEmployeeHandler(IUnitOfWork unitOfWork,IDepartmentGrpcService departmentGrpcService)
        {
            _unitOfWork = unitOfWork;
            _departmentGrpcService=departmentGrpcService;
        }

        public async Task<Unit> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            // التحقق من صحة DepartmentID وأنه موجود بالفعل في قاعدة البيانات
            var departmentExists = await _departmentGrpcService.CheckDepartmentExistsAsync(request.createEmployeeDTO.DepartmentID);
            if (!departmentExists)
            {
                throw new KeyNotFoundException($"Department with ID {request.createEmployeeDTO.DepartmentID} not found.");
            }
            // التحقق من أن AddressID يشير إلى عنوان موجود (في حالة كان AddressID مدخلاً)
            if (request.createEmployeeDTO.AddressID.HasValue)
            {
                var addressExists = await _unitOfWork.Addresses.GetEntityByIdAsync(request.createEmployeeDTO.AddressID.Value);
                if (addressExists == null)
                {
                    throw new KeyNotFoundException($"Address with ID {request.createEmployeeDTO.AddressID.Value} not found.");
                }
            }

            // التحقق من صحة ManagerID وأنه موجود بالفعل في قاعدة البيانات
            var managerExists = await _unitOfWork.Employees.GetEntityByIdAsync(request.createEmployeeDTO.ManagerID);
            if (managerExists == null)
            {
                throw new KeyNotFoundException($"Manager with ID {request.createEmployeeDTO.ManagerID} not found.");
            }

            // إزالة المسافات وجعل العملية غير حساسة لحالة الأحرف لتحويل JobTitle من نص إلى enum
            string jobTitleWithoutSpaces = request.createEmployeeDTO.JobTitle.Replace(" ", "", StringComparison.OrdinalIgnoreCase);

            if (!Enum.TryParse(typeof(JobTitle), jobTitleWithoutSpaces, true, out var jobTitleEnum))
            {
                throw new ArgumentException($"Invalid JobTitle value: {request.createEmployeeDTO.JobTitle}");
            }



            // إنشاء موظف جديد بناءً على الطلب (يجب أن تستخدم كائن Employee هنا)
            var newEmployee = new Employee // استخدام Employee بدلاً من CreateEmployeeDTO
            {
                FirstName = request.createEmployeeDTO.FirstName,
                LastName = request.createEmployeeDTO.LastName,
                MiddleName = request.createEmployeeDTO.MiddleName,
                JobTitle = (JobTitle)jobTitleEnum, // تعيين JobTitle المحول من النص كـ enum
                DepartmentID = request.createEmployeeDTO.DepartmentID,
                ManagerID = request.createEmployeeDTO.ManagerID,
                AddressID = request.createEmployeeDTO.AddressID,
                HireDate = request.createEmployeeDTO.HireDate,
                Salary = request.createEmployeeDTO.Salary
            };

            // إضافة الموظف الجديد إلى قاعدة البيانات
            await _unitOfWork.Employees.AddAsync(newEmployee);
            await _unitOfWork.CommitAsync(); // حفظ التغييرات في قاعدة البيانات

            return Unit.Value; // إرجاع وحدة القيمة لتشير إلى النجاح
        }


    }
}
