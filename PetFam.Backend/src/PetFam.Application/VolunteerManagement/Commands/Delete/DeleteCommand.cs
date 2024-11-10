using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Commands.Delete
{
    public record DeleteCommand(Guid Id) :ICommand;
}
