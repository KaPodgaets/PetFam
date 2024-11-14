using PetFam.BreedManagement.Application.SpeciesManagement.Commands.Create;

namespace PetFam.BreedManagement.Presentation.Requests
{
    public record CreateSpeciesRequest(string Name)
    {
        public CreateSpeciesCommand ToCommand()
        {
            return new CreateSpeciesCommand(Name);
        }
    }
}
