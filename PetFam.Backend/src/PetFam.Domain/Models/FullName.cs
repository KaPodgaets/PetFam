using System.Runtime.InteropServices;

namespace PetFam.Domain.Models
{
    public class FullName
    {
        public FullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name cannot be empty.");
            }

            FirstName = firstName;
            LastName = lastName;
        }

        public FullName(string firstName, string lastName, string patronymic)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name cannot be empty.");
            }

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
    }
}
