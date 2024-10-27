namespace Application.Features.Addresses.Queries.GetAddressById
{
    public class GetAddressByIdRequest:IRequest<Address>
    {
        public int AddressID {get;set;}
    }
}