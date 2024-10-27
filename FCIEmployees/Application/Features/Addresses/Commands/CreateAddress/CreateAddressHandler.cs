
namespace Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressHandler : IRequestHandler<CreateAddressRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAddressHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateAddressRequest request, CancellationToken cancellationToken)
        {
             // التحقق من أن TownID يشير إلى مدينة موجودة
            if (request.TownID.HasValue)
            {
                var townExists = await _unitOfWork.Towns.GetEntityByIdAsync(request.TownID.Value);
                if (townExists == null)
                {
                    throw new KeyNotFoundException($"Town with ID {request.TownID.Value} not found.");
                }
            }
            var newAddress = new Address
            {
                AddressText = request.AddressText,
                TownID = request.TownID
            };

            await _unitOfWork.Addresses.AddAsync(newAddress);
            await _unitOfWork.CommitAsync(); //   لحفظ التغييرات

            return Unit.Value; // إرجاع وحدة القيمة لتشير إلى النجاح
        }

       
    }
}
