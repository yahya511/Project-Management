

namespace Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeGrpcService _employeeGrpcService; // إضافة gRPC service


        public UpdateDepartmentHandler(IUnitOfWork unitOfWork,IEmployeeGrpcService employeeGrpcService)
        {
            _unitOfWork = unitOfWork;
            _employeeGrpcService = employeeGrpcService;
        }

        public async Task<Unit> Handle(UpdateDepartmentRequest request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(a=>a.DepartmentID==request.DepartmentID);

            if (department == null)
            {
                throw new Exception("Department not found."); // يمكنك تخصيص استثناء أفضل حسب الحاجة
            }
            var managerExists = await _employeeGrpcService.CheckManagerExistsAsync(request.ManagerID);
            if (!managerExists)
            {
                throw new KeyNotFoundException($"Manager with ID {request.ManagerID} not found.");
            }
            // تحديث الخصائص المطلوبة
            department.DepartmentName= request.DepartmentName;
            department.ManagerID = request.ManagerID;
           

            await _unitOfWork.Departments.UpdateAsync(department);
            await _unitOfWork.CommitAsync(); // استخدم DbContext لحفظ التغييرات

            return Unit.Value; // إرجاع وحدة القيمة لتشير إلى النجاح
        }
    }
}
