

namespace Application.Features.Projects.Commands.CreateProjectAndDepartment
{

    public class CreateProjectAndDepartmentRequest: IRequest<Unit>
    {
        public ProjectAndDepartmentDto ProjectAndDepartmentDto { get; set; }
    }


}


