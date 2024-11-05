using PetFam.Application.Interfaces;

namespace PetFam.Application.SpeciesManagement.Delete
{
    public record DeleteSpeciesCommand(Guid Id):ICommand;
}
