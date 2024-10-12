using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
{

    public class Pet : Entity<PetId>, ISoftDeletable
    {
        private bool _isDeleted = false;
        private Pet(PetId id) : base(id)
        {
        }

        private Pet(PetId petId,
            string nickName,
            Address address,
            SpeciesBreed speciesAndBreed) : base(petId)
        {
            NickName = nickName;
            Address = address;
            SpeciesAndBreed = speciesAndBreed;
        }

        public string NickName { get; private set; } = string.Empty;
        public SpeciesBreed SpeciesAndBreed { get; private set; }
        public string GeneralInfo { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;
        public string HealthInfo { get; private set; } = string.Empty;
        public Address Address { get; private set; } = null!;
        public double Weight { get; private set; }
        public double Height { get; private set; }
        public string PhoneNumber { get; private set; } = string.Empty;
        public bool IsCastrated { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool IsVaccinated { get; private set; }
        public PetStatus Status { get; private set; }
        public AccountInfo? AccountInfo { get; private set; }
        public DateTime CreateDate { get; private set; }
        public Gallery? Gallery { get; private set; }

        public static Result<Pet> Create(PetId petId,
            string nickName,
            Address address,
            SpeciesBreed speciesAndBreed)
        {
            if (petId.Value == Guid.Empty)
                return Errors.General.ValueIsInvalid(nameof(PetId));

            if (string.IsNullOrWhiteSpace(nickName))
                return Errors.General.ValueIsInvalid(nameof(NickName));

            if (nickName.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsInvalid(nameof(NickName));

            return new Pet(petId, nickName, address, speciesAndBreed);
        }

        public void Delete()
        {
            _isDeleted = true;
        }

        public void Restore()
        {
            _isDeleted = false;
        }
    }
}
