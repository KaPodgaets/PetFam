using PetFam.Shared.Abstractions;

namespace PetFam.Application.SpeciesManagement.Commands.Delete
{
    public record DeleteSpeciesCommand(Guid Id):ICommand;
}
