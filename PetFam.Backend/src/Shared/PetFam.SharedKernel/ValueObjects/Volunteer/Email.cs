using System.Text.RegularExpressions;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Shared.SharedKernel.ValueObjects.Volunteer
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
                return Errors.Errors.General.ValueIsInvalid(nameof(Email)).ToErrorList();

            if (email.Length > Constants.MAX_EMAIL_LENGTH)
                return Errors.Errors.General.ValueIsInvalid(nameof(Email)).ToErrorList();

            if (!Regex.IsMatch(email, EMAIL_REGEX, RegexOptions.IgnoreCase))
                return Errors.Errors.General.ValueIsInvalid(nameof(Email)).ToErrorList();

            return new Email(email);
        }
    }
}
