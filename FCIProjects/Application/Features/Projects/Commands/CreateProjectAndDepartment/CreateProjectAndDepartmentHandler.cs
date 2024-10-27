

namespace Application.Features.Projects.Commands.CreateProjectAndDepartment
{
    public class CreateProjectAndDepartmentHandler : IRequestHandler<CreateProjectAndDepartmentRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectAndDepartmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateProjectAndDepartmentRequest request, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Name = request.ProjectAndDepartmentDto.Name,
                Description = request.ProjectAndDepartmentDto.Description,
                StartDate = request.ProjectAndDepartmentDto.StartDate,
                EndDate = request.ProjectAndDepartmentDto.EndDate
            };

            var department = new Department
            {
                DepartmentName = request.ProjectAndDepartmentDto.DepartmentName,
                ManagerID = request.ProjectAndDepartmentDto.ManagerID
            };

            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
