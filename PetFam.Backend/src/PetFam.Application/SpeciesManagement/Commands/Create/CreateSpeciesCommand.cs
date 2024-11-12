using PetFam.Application.Interfaces;

namespace PetFam.Application.SpeciesManagement.Commands.Create
{
    public record CreateSpeciesCommand(string Name):ICommand;
}
