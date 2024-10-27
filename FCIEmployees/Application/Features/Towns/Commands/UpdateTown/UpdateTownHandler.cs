
namespace Application.Features.Towns.Commands.UpdateTown
{
    public class UpdateTownHandler : IRequestHandler<UpdateTownRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTownHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateTownRequest request, CancellationToken cancellationToken)
        {
            var town = await _unitOfWork.Towns.GetByIdAsync(a=>a.TownID==request.TownID);

            if (town == null)
            {
                throw new Exception("Town not found."); // يمكنك تخصيص استثناء أفضل حسب الحاجة
            }

            // تحديث الخصائص المطلوبة
            town.Name = request.Name; // افترض أن لديك خاصية اسمية

            await _unitOfWork.Towns.UpdateAsync(town);
            await _unitOfWork.CommitAsync(); // استخدم DbContext لحفظ التغييرات

            return Unit.Value; // إرجاع وحدة القيمة لتشير إلى النجاح
        }
    }
}
