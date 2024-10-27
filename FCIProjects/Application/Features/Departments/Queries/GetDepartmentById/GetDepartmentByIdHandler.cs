

namespace Application.Features.Departments.Queries.GetDepartmentById
{
    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdRequest, Department>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDepartmentByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Department> Handle(GetDepartmentByIdRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Departments.GetEntityByIdAsync(request.DepartmentID);
        }
    }

}
