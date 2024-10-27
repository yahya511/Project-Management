

namespace Application.Features.Towns.Queries.GetAllTowns
{
    public class GetAllTownsHandler : IRequestHandler<GetAllTownsRequest, IEnumerable<Town>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTownsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Town>> Handle(GetAllTownsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await _unitOfWork.Towns.GetAllAsync();
            }
            catch (Exception ex)
            {
                // يمكنك استخدام ILogger لتسجيل الخطأ
                // Log.Error(ex, "An error occurred while retrieving towns.");
                throw new Exception("Handle error occurred while retrieving towns.", ex);
            }
        }
    }
}
