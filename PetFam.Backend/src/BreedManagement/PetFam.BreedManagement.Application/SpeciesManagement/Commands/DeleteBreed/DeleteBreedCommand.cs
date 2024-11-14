namespace PetFam.BreedManagement.Application.SpeciesManagement.Commands.DeleteBreed;

public record DeleteBreedCommand(
    Guid SpeciesId,
    Guid BreedId) : ICommand;