using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Response;
using PetFam.Domain.Shared;

namespace PetFam.Api.Extensions
{
    public static class ResponseExtension
    {
        public static ActionResult ToResponse(this ValidationResult result) 
        {
            var validationErrors = result.Errors;

            var errors =
                    from validationError in validationErrors
                    select Error.Validation(validationError.ErrorCode, validationError.ErrorMessage);

            var errorList = new ErrorList(errors);

            var envelope = Envelope.Error(errorList);

            return new ObjectResult(envelope)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        public static ActionResult ToResponse(this Result result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(Envelope.Ok(result));
            }

            var distinctErrorTypes = result.Errors.Select(e => e.Type)
                .Distinct()
                .ToList();

            var statusCode = distinctErrorTypes.Count > 1
                ? StatusCodes.Status500InternalServerError
                : GetStatusCodeForErrorType(distinctErrorTypes.First());

            var envelope = Envelope.Error(result.Errors);

            return new ObjectResult(envelope)
            {
                StatusCode = statusCode,
            };
        }

        public static ActionResult<T> ToResponse<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(Envelope.Ok(result.Value));
            }

            var distinctErrorTypes = result.Errors.Select(e => e.Type)
                .Distinct()
                .ToList();

            var statusCode = distinctErrorTypes.Count > 1
                ? StatusCodes.Status500InternalServerError
                : GetStatusCodeForErrorType(distinctErrorTypes.First());

            var envelope = Envelope.Error(result.Errors);

            return new ObjectResult(envelope)
            {
                StatusCode = statusCode,
            };
        }

        private static int GetStatusCodeForErrorType(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}
