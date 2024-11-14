using PetFam.Shared.Abstractions;

namespace PetFam.Application.SpeciesManagement.Commands.Create
{
    public record CreateSpeciesCommand(string Name):ICommand;
}
