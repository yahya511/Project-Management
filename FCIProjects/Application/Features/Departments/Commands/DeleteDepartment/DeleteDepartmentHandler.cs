namespace Application.Features.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEmployeeGrpcService _employeeGrpcService; // إضافة gRPC service


        public DeleteDepartmentHandler(IUnitOfWork unitOfWork,IEmployeeGrpcService employeeGrpcService)
        {
            _unitOfWork = unitOfWork;
            _employeeGrpcService = employeeGrpcService;
        }

        public async Task<Unit> Handle(DeleteDepartmentRequest request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(a=>a.DepartmentID==request.DepartmentID);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {request.DepartmentID} not found.");
            }

            var managerExists = await _employeeGrpcService.CheckManagerExistsAsync(request.ManagerID);
            if (!managerExists)
            {
                throw new KeyNotFoundException($"Manager with ID {request.ManagerID} not found.");
            }

            /* // تحقق مما إذا كانت المدينة مرتبطة بعناوين
            var employeesDepartments = await _unitOfWork.employeeDepartment.GetAllAsync(em => em.DepartmentID == request.DepartmentID);
            if (employeesDepartments.Any())
            {
                throw new InvalidOperationException($"Cannot delete Department with ID {request.DepartmentID} because it has associated employeesDepartments.");
            } */

            // حذف المشروع
            await _unitOfWork.Departments.DeleteAsync(department.DepartmentID);

            // حفظ جميع التغييرات باستخدام UnitOfWork
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
