
namespace Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectRequest : IRequest<Unit>
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
