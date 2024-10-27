

namespace Employees.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressesController(IMediator mediator)
        {
            _mediator = mediator; // استخدام Mediator
        }

        
        // Create Town
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // Get Town by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var result = await _mediator.Send(new GetAddressByIdRequest { AddressID = id });
            return Ok(result);
        }

        // Get All Towns
        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
           var result = await _mediator.Send(new GetAllAddressesRequest());
            return Ok(result);
        }

        // Update Town
        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        // Delete Town
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            await _mediator.Send(new DeleteAddressRequest { AddressID = id });
            return NoContent();
        }
    }
}
