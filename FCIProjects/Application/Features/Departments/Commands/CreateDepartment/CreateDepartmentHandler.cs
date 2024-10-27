
namespace Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeGrpcService _employeeGrpcService; // إضافة gRPC service


        public CreateDepartmentHandler(IUnitOfWork unitOfWork,IEmployeeGrpcService employeeGrpcService)
        {
            _unitOfWork = unitOfWork;
            _employeeGrpcService = employeeGrpcService;
        }

        public async Task<Unit> Handle(CreateDepartmentRequest request, CancellationToken cancellationToken)
        {
            var managerExists = await _employeeGrpcService.CheckManagerExistsAsync(request.ManagerID);
            if (!managerExists)
            {
                throw new KeyNotFoundException($"Manager with ID {request.ManagerID} not found.");
            }
            var newDepartment = new Department
            {
                DepartmentName = request.DepartmentName,
                ManagerID = request.ManagerID
            };

            await _unitOfWork.Departments.AddAsync(newDepartment);
            await _unitOfWork.CommitAsync(); // استخدم DbContext لحفظ التغييرات

            return Unit.Value; // إرجاع وحدة القيمة لتشير إلى النجاح
        }

    }
}
