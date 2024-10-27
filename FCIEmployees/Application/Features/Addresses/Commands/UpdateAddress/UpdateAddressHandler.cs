
namespace Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressHandler : IRequestHandler<UpdateAddressRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAddressHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
        {
            var Address = await _unitOfWork.Addresses.GetByIdAsync(a=>a.AddressID==request.AddressID);

            if (Address == null)
            {
                throw new Exception("Address not found."); // يمكنك تخصيص استثناء أفضل حسب الحاجة
            }

            // التحقق من أن TownID يشير إلى مدينة موجودة
            if (request.TownID.HasValue) 
            {
                var townExists = await _unitOfWork.Towns.GetByIdAsync(a=>a.TownID==request.TownID.Value);
                if (townExists == null)
                {
                    throw new KeyNotFoundException($"Town with ID {request.TownID.Value} not found.");
                }
            }

            // تحديث الخصائص المطلوبة
            
             Address.AddressText=request.AddressText;
             Address.TownID=request.TownID;

            await _unitOfWork.Addresses.UpdateAsync(Address);
            await _unitOfWork.CommitAsync(); // استخدم DbContext لحفظ التغييرات

            return Unit.Value; // إرجاع وحدة القيمة لتشير إلى النجاح
        }

    }
}
