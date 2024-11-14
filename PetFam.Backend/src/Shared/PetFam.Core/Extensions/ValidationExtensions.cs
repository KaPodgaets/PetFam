using FluentValidation.Results;
using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Errors;

namespace PetFam.Shared.Extensions
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
