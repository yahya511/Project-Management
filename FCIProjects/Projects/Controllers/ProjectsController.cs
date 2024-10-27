


namespace FCIProjects.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator; // استخدام Mediator
        }


        // Create Project
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // Get Project by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var result = await _mediator.Send(new GetProjectByIdRequest { ProjectID = id });
            return Ok(result);
        }

        [HttpPost("CreateProjectAndDepartment")]
        public async Task<IActionResult> CreateProjectAndDepartment([FromBody] CreateProjectAndDepartmentRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        // Get All Projects
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
           
            var result = await _mediator.Send(new GetAllProjectsRequest());
            return Ok(result);
            
        }

        // Update Project
        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        // Delete Project
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _mediator.Send(new DeleteProjectRequest { ProjectID = id });
            return NoContent();
        }
    }
}
