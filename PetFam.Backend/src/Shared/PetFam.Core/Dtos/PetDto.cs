using PetFam.Shared.Dtos.ValueObjects;

namespace PetFam.Shared.Dtos
{
    public class PetDto
    {
        public Guid Id { get; init; }
        public Guid VolunteerId { get; init; }
        public string NickName { get; init; } = string.Empty;
        public SpeciesBreedDto SpeciesAndBreed { get; init; } = null!;
        public PetHealthInfoDto HealthInfo { get; init; } = null!;
        public PetGeneralInfoDto GeneralInfo { get; init; } = null!;
        public PetAddressDto Address { get; init; } = null!;
        public int Status { get; init; }
        public DateTime CreateDate { get; init; }
        public int Order { get; init; }
        public PetPhotoDto[] Photos { get; init; } = [];
    }
}
