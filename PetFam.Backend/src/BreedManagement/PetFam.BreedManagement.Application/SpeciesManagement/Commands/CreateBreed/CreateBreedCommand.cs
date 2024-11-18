using PetFam.Shared.Abstractions;

namespace PetFam.BreedManagement.Application.SpeciesManagement.Commands.CreateBreed
{
    public record CreateBreedCommand(Guid SpeciesId, string Name):ICommand;
}
