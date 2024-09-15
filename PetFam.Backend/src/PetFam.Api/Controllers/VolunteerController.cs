using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Controllers;
using PetFam.Api.Response;
using PetFam.Application.Extensions;
using PetFam.Application.Volunteers.Create;
using PetFam.Domain.Shared;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace PetFam.Application.Controllers
{
    public class VolunteerController : ApplicationController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] ICreateVolunteerHandler service,
            [FromServices] IValidator<CreateVolunteerRequest> validator,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors;

                var errors =
                    from validationError in validationErrors
                    let error = Error.Validation(validationError.ErrorCode, validationError.ErrorMessage)
                    select new ResponseError(error.Code, error.Message, validationError.PropertyName);

                var envelope = Envelope.Error(errors);
                return BadRequest(envelope);
            }

            var result = await service.Execute(request, cancellationToken);

            return result.ToResponse();
        }
    }
}
