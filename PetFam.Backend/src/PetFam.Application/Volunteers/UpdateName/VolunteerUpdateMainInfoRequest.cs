namespace PetFam.Application.Volunteers.UpdateName
{
    public record VolunteerUpdateMainInfoRequest(
        Guid Id,
        FullNameDto FullNameDto);
}
