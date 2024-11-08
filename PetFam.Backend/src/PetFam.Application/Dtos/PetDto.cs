using PetFam.Application.Dtos.ValueObjects;

namespace PetFam.Application.Dtos
{
    public class PetDto
    {
        public Guid Id { get; init; }
        public Guid VolunteerId { get; init; }
        public string NickName { get; init; } = string.Empty;
        public SpeciesBreedDto SpeciesAndBreed { get; init; } = null!;
        public int Status { get; init; }
        public DateTime CreateDate { get; init; }
        public int Order { get; init; }
        public PetPhotoDto[] Photos { get; init; } = [];
    }
}
