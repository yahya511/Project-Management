
namespace Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressRequest : IRequest<Unit>
    {
        public int AddressID { get; set; } 
        public string AddressText { get; set; }
        public int ? TownID { get; set; }
    }
}
