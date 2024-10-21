using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Extensions;
using PetFam.Api.Response;
using PetFam.Application.SpeciesManagement.Create;
using PetFam.Application.SpeciesManagement.CreateBreed;
using PetFam.Application.SpeciesManagement.Delete;
using PetFam.Domain.Shared;

namespace PetFam.Api.Controllers
{
    public class SpeciesController : ApplicationController
    {
        public SpeciesController(ILogger<ApplicationController> logger) : base(logger)
        {
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateSpecies(
            [FromServices] CreateSpeciesHandler handler,
            [FromServices] IValidator<CreateSpeciesRequest> validator,
            [FromBody] CreateSpeciesRequest request,
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

            var result = await handler.Handle(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteSpecies(
            [FromServices] DeleteSpeciesHandler handler,
            [FromServices] IValidator<DeleteSpeciesRequest> validator,
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var request = new DeleteSpeciesRequest(id);
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

            var result = await handler.Handle(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpPost("{speciesId:guid}/breed")]
        public async Task<ActionResult<Guid>> AddBreed(
            [FromServices] CreateBreedHandler handler,
            [FromServices] IValidator<CreateBreedRequest> validator,
            [FromRoute] Guid speciesId,
            [FromBody] string breedName,
            CancellationToken cancellationToken = default)
        {
            var request = new CreateBreedRequest(speciesId, breedName);

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

            var result = await handler.Handle(request, cancellationToken);

            return result.ToResponse();
        }
    }
}
