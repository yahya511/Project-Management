
namespace Application.Features.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressRequest : IRequest
    {
        public int AddressID { get; set; }
    }
}
