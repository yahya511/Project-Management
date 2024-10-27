
namespace Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectRequest : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}


