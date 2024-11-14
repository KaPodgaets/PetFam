using PetFam.Shared.Abstructions;

namespace PetFam.Application.SpeciesManagement.Commands.CreateBreed
{
    public record CreateBreedCommand(Guid SpeciesId, string Name):ICommand;
}
