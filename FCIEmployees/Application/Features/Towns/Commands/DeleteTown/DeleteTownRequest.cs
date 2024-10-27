
namespace Application.Features.Towns.Commands.DeleteTown
{
    public class DeleteTownRequest : IRequest
    {
        public int TownID { get; set; }
    }
}
