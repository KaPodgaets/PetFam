using PetFam.Application.SpeciesManagement.Create;

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
