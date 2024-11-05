using PetFam.Application.Interfaces;
using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateRequisites
{
    public record UpdateRequisitesCommand(
        Guid Id,
        IEnumerable<RequisiteDto>? Requisites):ICommand;
}
