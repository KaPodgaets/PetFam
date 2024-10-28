using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Extensions;
using PetFam.Application.VolunteerManagement.Create;
using PetFam.Application.VolunteerManagement.Delete;
using PetFam.Application.VolunteerManagement.UpdateMainInfo;
using PetFam.Application.VolunteerManagement.UpdateRequisites;
using PetFam.Application.VolunteerManagement.UpdateSocialMedia;

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
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
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
            var request = new UpdateMainInfoRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
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
            var request = new UpdateRequisitesRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
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
            var request = new UpdateSocialMediaRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            var result = await handler.Handle(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpDelete("{id:guid}/delete")]
        public async Task<ActionResult<Guid>> Delete(
            [FromRoute] Guid id,
            [FromServices] IDeleteHandler handler,
            [FromServices] IValidator<DeleteRequest> validator,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Try to delete volunteer with {id}",
                id);

            var request = new DeleteRequest(id);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            var result = await handler.Handle(request, cancellationToken);

            return result.ToResponse();
        }
    }
}
