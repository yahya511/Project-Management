

namespace Application.Features.Towns.Queries.GetTownById
{
    public class GetTownByIdRequest : IRequest<Town>
    {
        public int TownID { get; set; }
    }
}
