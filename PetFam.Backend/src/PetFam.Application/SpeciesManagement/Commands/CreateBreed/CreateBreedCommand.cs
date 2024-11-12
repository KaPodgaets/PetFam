using PetFam.Application.Interfaces;

namespace PetFam.Application.SpeciesManagement.Commands.CreateBreed
{
    public record CreateBreedCommand(Guid SpeciesId, string Name):ICommand;
}
