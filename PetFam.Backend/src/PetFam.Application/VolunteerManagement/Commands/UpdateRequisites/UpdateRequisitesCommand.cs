using PetFam.Application.Dtos.ValueObjects;
using PetFam.Shared.Abstructions;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateRequisites
{
    public record UpdateRequisitesCommand(
        Guid Id,
        IEnumerable<RequisiteDto>? Requisites):ICommand;
}
