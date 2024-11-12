using PetFam.Application.Interfaces;

namespace PetFam.Application.SpeciesManagement.Commands.Delete
{
    public record DeleteSpeciesCommand(Guid Id):ICommand;
}
