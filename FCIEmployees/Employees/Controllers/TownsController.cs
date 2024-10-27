

namespace Employees.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TownsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TownsController(IMediator mediator)
        {
            _mediator = mediator; // استخدام Mediator
        }

        

        // Create Town
        [HttpPost]
        public async Task<IActionResult> CreateTown([FromBody] CreateTownRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // Get Town by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTownById(int id)
        {
            var result = await _mediator.Send(new GetTownByIdRequest { TownID = id });
            return Ok(result);
        }

        // Get All Towns
        [HttpGet]
        public async Task<IActionResult> GetAllTowns()
        {
            try
            {
                var result = await _mediator.Send(new GetAllTownsRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                // يمكنك استخدام ILogger لتسجيل الخطأ
                // Log.Error(ex, "An error occurred while fetching all towns.");
                return StatusCode(500, $"Controller Internal server error: {ex.Message}");
            }
        }

        // Update Town
        [HttpPut]
        public async Task<IActionResult> UpdateTown([FromBody] UpdateTownRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        // Delete Town
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTown(int id)
        {
            await _mediator.Send(new DeleteTownRequest { TownID = id });
            return NoContent();
        }
    }
}
