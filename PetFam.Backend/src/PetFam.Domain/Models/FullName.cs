using System.Runtime.InteropServices;

namespace PetFam.Domain.Models
{
    public class FullName
    {
        public FullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("First name and last name cannot be empty.");
            }

            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override int GetHashCode() =>
            HashCode.Combine(FirstName, LastName);

        public override string ToString() =>
            $"{FirstName} {LastName}";
    }
}
