using PetFam.Application.SpeciesManagement.Commands.Create;

namespace PetFam.Api.Requests.Species
{
    public record CreateSpeciesRequest(string Name)
    {
        public CreateSpeciesCommand ToCommand()
        {
            return new CreateSpeciesCommand(Name);
        }
    }
}
