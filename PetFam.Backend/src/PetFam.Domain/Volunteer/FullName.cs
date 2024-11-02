using PetFam.Domain.Shared;

namespace PetFam.Domain.Volunteer
{
    public record FullName
    {
        private FullName(string firstName, string lastName, string? patronymic)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string? Patronymic { get; private set; }

        public override int GetHashCode() =>
            HashCode.Combine(FirstName, LastName);

        public override string ToString() =>
            $"{FirstName} {LastName}";

        public static Result<FullName> Create(string firstName,
            string lastName,
            string? patronymic)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Errors.General.ValueIsInvalid(nameof(FirstName)).ToErrorList();
            if (string.IsNullOrWhiteSpace(lastName))
                return Errors.General.ValueIsInvalid(nameof(LastName)).ToErrorList();

            return new FullName(firstName, lastName, patronymic);
        }
    }
}
