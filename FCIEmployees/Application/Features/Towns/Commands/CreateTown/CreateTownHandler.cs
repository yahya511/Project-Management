
namespace Application.Features.Towns.Commands.CreateTown
{
    public class CreateTownHandler : IRequestHandler<CreateTownRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTownHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateTownRequest request, CancellationToken cancellationToken)
        {
            var newTown = new Town
            {
                Name = request.Name // افترض أن لديك خاصية اسمية
                // إضافة أي خصائص أخرى لازمة
            };

            await _unitOfWork.Towns.AddAsync(newTown);
            await _unitOfWork.CommitAsync(); // استخدم DbContext لحفظ التغييرات

            return Unit.Value; // إرجاع وحدة القيمة لتشير إلى النجاح
        }
    }
}
