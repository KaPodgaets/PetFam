using PetFam.Domain.Shared;
using System.Text.RegularExpressions;

namespace PetFam.Domain.Volunteer
{
    public record Email
    {
        private const string EMAIL_REGEX = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        private Email(string value)
        {
            Value = value;
        }
        public string Value { get; }

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Errors.General.ValueIsInvalid(nameof(Email));

            if (email.Length > Constants.MAX_EMAIL_LENGTH)
                return Errors.General.ValueIsInvalid(nameof(Email));

            if (!Regex.IsMatch(email, EMAIL_REGEX, RegexOptions.IgnoreCase))
                return Errors.General.ValueIsInvalid(nameof(Email));

            return new Email(email);
        }
    }
}
