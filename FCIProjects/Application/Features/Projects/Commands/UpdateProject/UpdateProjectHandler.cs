
namespace Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(a=>a.ProjectID==request.ProjectID);

            if (project == null)
            {
                throw new Exception("Project not found."); // يمكنك تخصيص استثناء أفضل حسب الحاجة
            }

            // تحديث الخصائص المطلوبة
            project.Name = request.Name;
            project.Description = request.Description;
            project.StartDate = request.StartDate;
            project.EndDate = request.EndDate;

            await _unitOfWork.Projects.UpdateAsync(project);
            await _unitOfWork.CommitAsync(); // استخدم DbContext لحفظ التغييرات

            return Unit.Value; // إرجاع وحدة القيمة لتشير إلى النجاح
        }
    }
}
