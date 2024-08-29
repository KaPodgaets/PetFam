namespace PetFam.Domain.Models
{
    public class Pet
    {
        private readonly List<PetPhoto> _petPhotos = [];

        public Guid Id { get; private set; }
        public string NickName { get; private set; } = string.Empty;
        public string Species { get; private set; } = string.Empty;
        public string GeneralInfo { get; private set; } = string.Empty;
        public string Breed { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;
        public string HealthInfo { get; private set; } = string.Empty;
        public string Address { get; private set; } = string.Empty;
        public double Weight { get; private set; }
        public double Height { get; private set; }
        public string PhoneNumber { get; private set; } = string.Empty;
        public bool IsCastrated { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool IsVaccinated { get; private set; }
        public PetStatus Status { get; private set; }
        public AccountInfo? AccountInfo { get; private set; }
        public DateTime CreateDate { get; private set; }
        public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;
    }
}
