
namespace Application.Features.Departments.Queries.GetAllDepartments
{
    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsRequest, IEnumerable<Department>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDepartmentsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Department>> Handle(GetAllDepartmentsRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Departments.GetAllAsync();
        }
    }
}
