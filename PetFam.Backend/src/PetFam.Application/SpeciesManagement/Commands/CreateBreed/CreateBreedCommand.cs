using PetFam.Application.Interfaces;

namespace PetFam.Application.SpeciesManagement.CreateBreed
{
    public record CreateBreedCommand(Guid SpeciesId, string Name):ICommand;
}
