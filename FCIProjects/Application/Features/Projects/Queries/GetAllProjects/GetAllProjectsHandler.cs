
namespace Application.Features.Projects.Queries.GetAllProjects
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsRequest, IEnumerable<Project>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProjectsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Project>> Handle(GetAllProjectsRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Projects.GetAllAsync();
        }
    }
}
