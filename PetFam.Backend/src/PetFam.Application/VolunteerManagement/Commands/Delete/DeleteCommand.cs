using PetFam.Shared.Abstructions;

namespace PetFam.Application.VolunteerManagement.Commands.Delete
{
    public record DeleteCommand(Guid Id) :ICommand;
}
