using PetFam.Shared.Abstractions;
using PetFam.Shared.Dtos.ValueObjects;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateRequisites
{
    public record UpdateRequisitesCommand(
        Guid Id,
        IEnumerable<RequisiteDto>? Requisites):ICommand;
}
