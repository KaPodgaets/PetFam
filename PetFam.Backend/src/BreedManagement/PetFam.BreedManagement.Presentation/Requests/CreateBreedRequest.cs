using PetFam.BreedManagement.Application.SpeciesManagement.Commands.CreateBreed;

namespace PetFam.BreedManagement.Presentation.Requests
{
    public record CreateBreedRequest(Guid SpeciesId, string Name)
    {
        public CreateBreedCommand ToCommand()
        {
            return new CreateBreedCommand(SpeciesId, Name);
        }
    };
}
