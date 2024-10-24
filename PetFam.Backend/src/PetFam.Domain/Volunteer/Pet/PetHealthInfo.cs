using PetFam.Domain.Shared;

namespace PetFam.Domain.Volunteer.Pet
{
    public record PetHealthInfo
    {
        private PetHealthInfo(
            string comment,
            bool isCastrated,
            DateTime birthDate,
            bool isVaccinated)
        {
            Comment = comment;
            IsCastrated = isCastrated;
            BirthDate = birthDate;
            IsVaccinated = isVaccinated;
        }

        public string Comment { get; set; }
        public bool IsCastrated { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsVaccinated { get; set; }

        public static Result<object> Create(
            string comment,
            bool isCastrated,
            DateTime birthDate,
            bool isVaccinated)
        {
            return new PetHealthInfo(comment, isCastrated, birthDate, isVaccinated);
        }
    }
}
