namespace Application.Features.Projects.Queries.GetProjectById
{
    public class GetProjectByIdRequest : IRequest<Project>
    {
        public int ProjectID { get; set; }
    }
}
