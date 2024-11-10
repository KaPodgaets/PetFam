using PetFam.Application.Interfaces;

namespace PetFam.Application.SpeciesManagement.Create
{
    public record CreateSpeciesCommand(string Name):ICommand;
}
