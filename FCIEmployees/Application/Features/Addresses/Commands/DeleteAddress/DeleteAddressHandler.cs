

namespace Application.Features.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressHandler : IRequestHandler<DeleteAddressRequest, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteAddressHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
        {
            var Address=await _unitOfWork.Addresses.GetByIdAsync(a=>a.AddressID==request.AddressID);
            if(Address!=null)
            {
                await _unitOfWork.Addresses.DeleteAsync(Address.AddressID);
                await _unitOfWork.CommitAsync();

            }
            return Unit.Value;
        }
    }
}
