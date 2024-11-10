using PetFam.Application.Dtos.ValueObjects;
using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateRequisites
{
    public record UpdateRequisitesCommand(
        Guid Id,
        IEnumerable<RequisiteDto>? Requisites):ICommand;
}
