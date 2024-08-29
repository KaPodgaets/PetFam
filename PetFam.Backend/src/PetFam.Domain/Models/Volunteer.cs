namespace PetFam.Domain.Models
{
    public class Volunteer
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string GeneralInformation { get; set; } = string.Empty;
        public int AgesOfExpirience { get; set; }
        public List<SocialMediaLink> Links { get; set; } = [];
        public List<Requisite> Requisites { get; set; } = [];
        public List<Pet> Pets { get; set; } = [];
        public int PetsFoundedHomeCount =>
            (Pets.Where(x => x.Status == PetStatus.Adopted).Count());
        public int PetsLookingForHomeCount =>
            (Pets.Where(x => x.Status == PetStatus.LookingForHome).Count());
        public int PetsOnTreatment =>
            (Pets.Where(x => x.Status == PetStatus.OnTreatment).Count());

        public void AddPet(Pet pet)
        {
            if (Pets.Any(p => p.Id == pet.Id))
            {
                throw new InvalidOperationException("This pet is already assigned to the volunteer.");
            }

            Pets.Add(pet);
        }

        public void RemovePet(Guid petId)
        {
            var pet = Pets.FirstOrDefault(p => p.Id == petId);
            if (pet != null)
            {
                Pets.Remove(pet);
            }
        }

        public void UpdatePetStatus(Guid petId, PetStatus newStatus)
        {
            var pet = Pets.FirstOrDefault(p => p.Id == petId);

            if (pet != null)
            {
                pet.UpdateStatus(newStatus);
            }
        }
    }
}
