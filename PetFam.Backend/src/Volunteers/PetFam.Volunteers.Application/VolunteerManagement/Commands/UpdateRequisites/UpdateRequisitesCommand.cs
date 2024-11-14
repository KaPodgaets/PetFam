namespace PetFam.Volunteers.Application.VolunteerManagement.Commands.UpdateRequisites
{
    public record UpdateRequisitesCommand(
        Guid Id,
        IEnumerable<RequisiteDto>? Requisites):ICommand;
}
