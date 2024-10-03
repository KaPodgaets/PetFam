namespace PetFam.Application.Volunteers.UpdateName
{
    public record VolunteerUpdateNameRequest(
        Guid Id,
        FullNameDto FullNameDto);
}
