using PetFam.Shared.Abstructions;

namespace PetFam.Application.SpeciesManagement.Commands.Delete
{
    public record DeleteSpeciesCommand(Guid Id):ICommand;
}
