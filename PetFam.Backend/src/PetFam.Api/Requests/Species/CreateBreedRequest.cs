using PetFam.Application.SpeciesManagement.CreateBreed;

namespace PetFam.Api.Requests.Species
{
    public record CreateBreedRequest(Guid SpeciesId, string Name)
    {
        public CreateBreedCommand ToCommand()
        {
            return new CreateBreedCommand(SpeciesId, Name);
        }
    };
}
