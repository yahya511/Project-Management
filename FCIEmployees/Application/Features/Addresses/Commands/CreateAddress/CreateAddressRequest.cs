
namespace Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressRequest : IRequest<Unit>
    {
        public string AddressText { get; set; }
        public int? TownID { get; set; }    
    }
}


