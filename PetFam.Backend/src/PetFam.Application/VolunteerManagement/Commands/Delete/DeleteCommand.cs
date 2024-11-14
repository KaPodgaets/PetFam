using PetFam.Shared.Abstractions;

namespace PetFam.Application.VolunteerManagement.Commands.Delete
{
    public record DeleteCommand(Guid Id) :ICommand;
}
