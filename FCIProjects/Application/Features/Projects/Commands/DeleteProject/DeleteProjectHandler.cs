namespace Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(a=>a.ProjectID==request.ProjectID);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {request.ProjectID} not found.");
            }

            /* // تحقق مما إذا كانت المدينة مرتبطة بعناوين
            var employeesProjects = await _unitOfWork.employeeProject.GetAllAsync(em => em.ProjectID == request.ProjectID);
            if (employeesProjects.Any())
            {
                throw new InvalidOperationException($"Cannot delete Project with ID {request.ProjectID} because it has associated employeesProjects.");
            } */

            // حذف المشروع
            await _unitOfWork.Projects.DeleteAsync(project.ProjectID);

            // حفظ جميع التغييرات باستخدام UnitOfWork
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
