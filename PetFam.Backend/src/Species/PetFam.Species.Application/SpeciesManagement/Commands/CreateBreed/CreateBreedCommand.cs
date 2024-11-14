namespace PetFam.Species.Application.SpeciesManagement.Commands.CreateBreed
{
    public record CreateBreedCommand(Guid SpeciesId, string Name):ICommand;
}
