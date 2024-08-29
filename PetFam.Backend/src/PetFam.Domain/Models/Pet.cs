namespace PetFam.Domain.Models
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string GeneralInfo { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string HealthInfo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Weight { get; set; }
        public int Height { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsCastrated { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsVaccinated { get; set; }
        public PetStatus Status { get; set; }
        public string AccountInfo { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }

        public void UpdateStatus(PetStatus newStatus)
        {
            Status = newStatus;
        }

        public void UpdateAddress(string newAddress)
        {
            if (string.IsNullOrWhiteSpace(newAddress))
            {
                throw new ArgumentException("Address cannot be empty");
            }

            Address = newAddress;
        }

        public void UpdateWeight(int newWeight)
        {
            if (newWeight <= 0)
            {
                throw new ArgumentException("Weight must be greater than zero");
            }

            Weight = newWeight;
        }

        public void UpdateHeight(int newHeight)
        {
            if (newHeight <= 0)
            {
                throw new ArgumentException("Height must be greater than zero");
            }

            Height = newHeight;
        }
    }
}
