using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Shared.SharedKernel.ValueObjects.Pet
{
    public record PetHealthInfo
    {
        private PetHealthInfo(
            string comment,
            bool isCastrated,
            DateTime birthDate,
            bool isVaccinated,
            int age)
        {
            Comment = comment;
            IsCastrated = isCastrated;
            BirthDate = birthDate;
            IsVaccinated = isVaccinated;
            Age = age;
        }
        
        public string Comment { get; set; }
        public bool IsCastrated { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsVaccinated { get; set; }
        public int Age { get; set; }

        public static Result<PetHealthInfo> Create(
            string comment,
            bool isCastrated,
            DateTime birthDate,
            bool isVaccinated,
            int age)
        {
            return new PetHealthInfo(comment, isCastrated, birthDate, isVaccinated, age);
        }
    }
}
