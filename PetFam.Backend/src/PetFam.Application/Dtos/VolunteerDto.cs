using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.Dtos
{
    public class VolunteerDto
    {
        public Guid Id { get; init; }
        public string Email { get; init; } = string.Empty;
        public int AgesOfExperience { get; init; }
    }
}
