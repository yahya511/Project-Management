
namespace Application.Features.Addresses.Queries.GetAllAddresses
{
    public class GetAllAddressesHandeler : IRequestHandler<GetAllAddressesRequest, IEnumerable<Address>>
    {   
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAddressesHandeler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<IEnumerable<Address>> Handle(GetAllAddressesRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Addresses.GetAllAsync();
        }
    }
}