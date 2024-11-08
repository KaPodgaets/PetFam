namespace PetFam.Application.VolunteerManagement.ValueObjects
{
    public record PetHealthInfoDto(
        string Comment,
        bool IsCastrated,
        DateTime BirthDate,
        bool IsVaccinated);
}
