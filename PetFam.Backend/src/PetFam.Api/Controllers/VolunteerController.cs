﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Extensions;
using PetFam.Api.Response;
using PetFam.Application.Volunteers.Create;
using PetFam.Application.Volunteers.UpdateMainInfo;
using PetFam.Application.Volunteers.UpdateRequisites;
using PetFam.Application.Volunteers.UpdateSocialMedia;
using PetFam.Domain.Shared;

namespace PetFam.Api.Controllers
{
    public class VolunteerController : ApplicationController
    {
        public VolunteerController(ILogger<VolunteerController> logger)
            : base(logger)
        {
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] ICreateVolunteerHandler handler,
            [FromServices] IValidator<CreateVolunteerRequest> validator,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Create volunteer request");

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors;

                var errors =
                    from validationError in validationErrors
                    let error = Error.Validation(validationError.ErrorCode, validationError.ErrorMessage)
                    select new ResponseError(error.Code, error.Message, validationError.PropertyName);

                var envelope = Envelope.Error(errors);

                _logger.LogInformation(
                    "Validation error occured while creating. Errors: {errors}",
                    envelope.Errors);

                return BadRequest(envelope);
            }

            var result = await handler.Handle(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult<Guid>> UpdateMainInfo(
            [FromRoute] Guid id,
            [FromServices] IUpdateMainInfoHandler handler,
            [FromServices] IValidator<UpdateMainInfoRequest> validator,
            [FromBody] UpdateMainInfoDto dto,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Try to update name for volunteer with {id}",
                id);

            var request = new UpdateMainInfoRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors;

                var errors =
                    from validationError in validationErrors
                    let error = Error.Validation(validationError.ErrorCode, validationError.ErrorMessage)
                    select new ResponseError(error.Code, error.Message, validationError.PropertyName);

                var envelope = Envelope.Error(errors);

                _logger.LogInformation(
                    "Validation error occured while updating. Errors: {errors}",
                    envelope.Errors);

                return BadRequest(envelope);
            }

            var result = await handler.Handle(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpPut("{id:guid}/requisits")]
        public async Task<ActionResult<Guid>> UpdateRequisits(
            [FromRoute] Guid id,
            [FromServices] IUpdateRequisitesHandler handler,
            [FromServices] IValidator<UpdateRequisitesRequest> validator,
            [FromBody] UpdateRequisitesDto dto,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Try to update name for volunteer with {id}",
                id);

            var request = new UpdateRequisitesRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors;

                var errors =
                    from validationError in validationErrors
                    let error = Error.Validation(validationError.ErrorCode, validationError.ErrorMessage)
                    select new ResponseError(error.Code, error.Message, validationError.PropertyName);

                var envelope = Envelope.Error(errors);

                _logger.LogInformation(
                    "Validation error occured while updating. Errors: {errors}",
                    envelope.Errors);

                return BadRequest(envelope);
            }

            var result = await handler.Handle(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpPut("{id:guid}/social-media")]
        public async Task<ActionResult<Guid>> UpdateSocialMedia(
            [FromRoute] Guid id,
            [FromServices] IUpdateSocialMediaHandler handler,
            [FromServices] IValidator<UpdateSocialMediaRequest> validator,
            [FromBody] UpdateSocialMediaDto dto,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Try to update name for volunteer with {id}",
                id);

            var request = new UpdateSocialMediaRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors;

                var errors =
                    from validationError in validationErrors
                    let error = Error.Validation(validationError.ErrorCode, validationError.ErrorMessage)
                    select new ResponseError(error.Code, error.Message, validationError.PropertyName);

                var envelope = Envelope.Error(errors);

                _logger.LogInformation(
                    "Validation error occured while updating. Errors: {errors}",
                    envelope.Errors);

                return BadRequest(envelope);
            }

            var result = await handler.Handle(request, cancellationToken);

            return result.ToResponse();
        }
    }
}
