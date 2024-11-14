using FluentValidation.Results;
using PetFam.Shared.Shared;

namespace PetFam.Application.Extensions
{
    public static class ValidationExtensions
    {
        public static ErrorList ToErrorList(this ValidationResult result)
        {
            var validationErrors = result.Errors;

            var errors =
                    from validationError in validationErrors
                    select Error.Validation(validationError.ErrorCode, validationError.ErrorMessage, validationError.PropertyName);

            return errors.ToList();
        }
    }
}
