using PetFam.Shared.Abstractions;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.Delete
{
    public record DeleteCommand(Guid Id) :ICommand;
}
