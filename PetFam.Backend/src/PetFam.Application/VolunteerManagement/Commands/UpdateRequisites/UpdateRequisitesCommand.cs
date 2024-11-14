using PetFam.Application.Dtos.ValueObjects;
using PetFam.Shared.Abstractions;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateRequisites
{
    public record UpdateRequisitesCommand(
        Guid Id,
        IEnumerable<RequisiteDto>? Requisites):ICommand;
}
