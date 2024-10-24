namespace PetFam.Domain.Volunteer.Pet
{
    public record PetHealthInfo(
        string Comment,
        bool IsCastrated,
        DateTime BirthDate,
        bool IsVaccinated);
}
