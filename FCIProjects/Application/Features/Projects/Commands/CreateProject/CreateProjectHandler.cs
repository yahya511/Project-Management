
namespace Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            var newProject = new Project
            {
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            await _unitOfWork.Projects.AddAsync(newProject);
            await _unitOfWork.CommitAsync(); // استخدم DbContext لحفظ التغييرات

            return Unit.Value; // إرجاع وحدة القيمة لتشير إلى النجاح
        }

    }
}
