using PetFam.Shared.Abstructions;

namespace PetFam.Application.SpeciesManagement.Commands.Create
{
    public record CreateSpeciesCommand(string Name):ICommand;
}
