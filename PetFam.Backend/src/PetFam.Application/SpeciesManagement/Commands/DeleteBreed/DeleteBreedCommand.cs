using PetFam.Application.Interfaces;

namespace PetFam.Application.SpeciesManagement.Commands.DeleteBreed;

public record DeleteBreedCommand(
    Guid SpeciesId,
    Guid BreedId) : ICommand;