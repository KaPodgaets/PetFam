using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.Dtos
{
    public class VolunteerDto
    {
        public Guid Id { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public int AgesOfExperience { get; init; }
        public PetDto[] Pets { get; init; } = [];
    }
}
