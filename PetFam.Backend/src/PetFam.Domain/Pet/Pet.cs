using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
{

    public class Pet : Entity<PetId>
    {
        private Pet(PetId id) : base(id)
        {
        }

        private Pet(PetId petId, string nickName, Address address) : base(petId)
        {
            NickName = nickName;
            Address = address;
        }

        public string NickName { get; private set; } = string.Empty;
        public string Species { get; private set; } = string.Empty;
        public string GeneralInfo { get; private set; } = string.Empty;
        public string Breed { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;
        public string HealthInfo { get; private set; } = string.Empty;
        public Address Address { get; private set; }
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
            Address address)
        {
            if (petId.Value == Guid.Empty)
                return "Can't create Pet model with Empty id";

            if (string.IsNullOrWhiteSpace(nickName))
                return "Pet's nickname could not be empty";

            if (nickName.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return $"Pet's nickname could not be longer that {Constants.MAX_LOW_TEXT_LENGTH} symbols";

            return new Pet(petId, nickName, address);
        }
    }
}
