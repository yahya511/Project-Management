
namespace Application.Features.Towns.Commands.CreateTown
{
    public class CreateTownRequest : IRequest<Unit>
    {
        public string Name { get; set; }
    }
}


