

namespace Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentGrpcService _departmentGrpcService; // إضافة gRPC service

        public DeleteEmployeeHandler(IUnitOfWork unitOfWork,IDepartmentGrpcService departmentGrpcService)
        {
            _unitOfWork = unitOfWork;
            _departmentGrpcService=departmentGrpcService;
        }

        public async Task<Unit> Handle(DeleteEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Employees.GetEntityByIdAsync(request.EmployeeID);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.EmployeeID} not found.");
            }

            // التحقق من صحة DepartmentID وأنه موجود بالفعل في قاعدة البيانات
            var managerExists = await _departmentGrpcService.CheckManagerExistsAsync(request.EmployeeID);
            if (managerExists)
            {
                throw new InvalidOperationException($"Cannot delete employee with ID {request.EmployeeID} because it has associated departments .");
            }
            // تحقق مما إذا كان الموظف مرتبط بمدير
            var managers = await _unitOfWork.Employees.GetAllAsync(a => a.ManagerID == request.EmployeeID);
            if (managers.Any())
            {
                throw new InvalidOperationException($"Cannot delete employee with ID {request.EmployeeID} because it has associated managers.");
            }

            
            await _unitOfWork.Employees.DeleteAsync(employee.EmployeeID);
            await _unitOfWork.CommitAsync(); // استخدم DbContext لحفظ التغييرات
           

            return Unit.Value;
        }
    }
}
