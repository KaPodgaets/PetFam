namespace PetFam.Domain.Models
{
    public class Pet
    {
        private readonly List<PetPhoto> _petPhotos = [];

        public Guid Id { get; }
        public string NickName { get; } = string.Empty;
        public string Species { get; } = string.Empty;
        public string GeneralInfo { get; } = string.Empty;
        public string Breed { get; } = string.Empty;
        public string Color { get; } = string.Empty;
        public string HealthInfo { get; } = string.Empty;
        public string Address { get; } = string.Empty;
        public double Weight { get; }
        public double Height { get; }
        public string PhoneNumber { get; } = string.Empty;
        public bool IsCastrated { get; }
        public DateTime BirthDate { get; }
        public bool IsVaccinated { get; }
        public PetStatus Status { get; }
        public AccountInfo? AccountInfo { get; }
        public DateTime CreateDate { get; }
        public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;
    }
}
