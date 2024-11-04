using PetFam.Application.VolunteerManagement.ValueObjects;
using PetFam.Domain.Volunteer.Pet;

namespace PetFam.Application.Dtos
{
    public class PetDto
    {
        public Guid Id { get; init; }
        public Guid VolunteerId { get; init; }
        public string NickName { get; init; }
        public Guid SpeciesId { get; init; }
        public Guid BreedId { get; init; }
        public PetStatus Status { get; init; }
        public PetGeneralInfoDto GeneralInfo { get; init; }
        public PetHealthInfoDto HealthInfo { get; init; }
        public PetAddressDto Address { get; init; }
        public AccountInfoDto AccountInfo { get; init; }
        public DateTime CreateDate { get; init; }
        int Order { get; init; }
    }
}
