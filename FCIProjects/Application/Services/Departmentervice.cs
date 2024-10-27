using Grpc.Core;
using Proto;

namespace Application.Services
    {
        public class DepartmentService : Proto.DepartmentService.DepartmentServiceBase
    {
        private readonly IDepartmentRepository _departmentRepository;

            public DepartmentService(IDepartmentRepository departmentRepository)
            {
                _departmentRepository = departmentRepository;
            }   
    

        public override async Task<DepartmentExistResponse> DepartmentExist(
            DepartmentExistRequest request, ServerCallContext context)
        {
            var exists = await _departmentRepository.ExistsAsync(request.DepartmentId);
            return new DepartmentExistResponse { Exists = exists };
        }

        public override async Task<ManagerExistResponse> ManagerExist(
            ManagerExistRequest request, ServerCallContext context)
        {
            var exists = await _departmentRepository.ManagerExistsAsync(request.ManagerId);
            return new ManagerExistResponse { Exists = exists };
        }
    }
}
