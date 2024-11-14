namespace PetFam.Shared.ValueObjects.Pet
{
    public record PetGeneralInfo
    {
        public PetGeneralInfo(
            string comment,
            string color,
            double weight,
            double height,
            string phoneNumber)
        {
            Comment = comment;
            Color = color;
            Weight = weight;
            Height = height;
            PhoneNumber = phoneNumber;
        }

        public string Comment { get; set; }
        public string Color { get; set; }
        public double Weight { get; set; } = 0.0;
        public double Height { get; set; } 
        public string PhoneNumber { get; set; }

        public static Result<PetGeneralInfo> Create(
            string comment,
            string color,
            double weight,
            double height,
            string phoneNumber)
        {
            return new PetGeneralInfo(comment, color, weight, height, phoneNumber);
        }
    }
}
