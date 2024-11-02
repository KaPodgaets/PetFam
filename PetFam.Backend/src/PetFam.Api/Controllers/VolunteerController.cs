using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Contracts;
using PetFam.Api.Extensions;
using PetFam.Api.Processors;
using PetFam.Application.FileProvider;
using PetFam.Application.VolunteerManagement.Create;
using PetFam.Application.VolunteerManagement.Delete;
using PetFam.Application.VolunteerManagement.PetManagement.AddPhotos;
using PetFam.Application.VolunteerManagement.PetManagement.Create;
using PetFam.Application.VolunteerManagement.UpdateMainInfo;
using PetFam.Application.VolunteerManagement.UpdateRequisites;
using PetFam.Application.VolunteerManagement.UpdateSocialMedia;
using PetFam.Infrastructure.Options;

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
            [FromServices] IValidator<CreateVolunteerCommand> validator,
            [FromBody] CreateVolunteerCommand request,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            var result = await handler.Execute(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult<Guid>> UpdateMainInfo(
            [FromRoute] Guid id,
            [FromServices] IUpdateMainInfoHandler handler,
            [FromServices] IValidator<UpdateMainInfoCommand> validator,
            [FromBody] UpdateMainInfoDto dto,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateMainInfoCommand(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            var result = await handler.Execute(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpPut("{id:guid}/requisites")]
        public async Task<ActionResult<Guid>> UpdateRequisites(
            [FromRoute] Guid id,
            [FromServices] IUpdateRequisitesHandler handler,
            [FromServices] IValidator<UpdateRequisitesCommand> validator,
            [FromBody] UpdateRequisitesDto dto,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateRequisitesCommand(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            var result = await handler.Execute(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpPut("{id:guid}/social-media")]
        public async Task<ActionResult<Guid>> UpdateSocialMedia(
            [FromRoute] Guid id,
            [FromServices] IUpdateSocialMediaHandler handler,
            [FromServices] IValidator<UpdateSocialMediaCommand> validator,
            [FromBody] UpdateSocialMediaDto dto,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateSocialMediaCommand(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            var result = await handler.Execute(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpDelete("{id:guid}/delete")]
        public async Task<ActionResult<Guid>> Delete(
            [FromRoute] Guid id,
            [FromServices] IDeleteHandler handler,
            [FromServices] IValidator<DeleteCommand> validator,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Try to delete volunteer with {id}",
                id);

            var request = new DeleteCommand(id);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            var result = await handler.Execute(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpPost("{id:guid}/add-pet")]
        public async Task<ActionResult<Guid>> AddNewPet(
            [FromRoute] Guid id,
            [FromServices] CreatePetHandler handler,
            [FromServices] IValidator<CreatePetCommand> validator,
            [FromBody] CreatePetDto dto,
            CancellationToken cancellationToken = default)
        {
            var request = new CreatePetCommand(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            var result = await handler.Execute(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpPost("{id:guid}/add-photos/{petId:guid}")]
        public async Task<ActionResult<string>> AddPetPhotos(
            [FromRoute] Guid id,
            [FromRoute] Guid petId,
            [FromServices] PetAddPhotosHandler handler,
            [FromServices] IValidator<PetAddPhotosCommand> validator,
            [FromForm] IFormFileCollection formFiles,
            CancellationToken cancellationToken = default)
        {
            
            await using var fileProcessor = new FormFileProcessor();
            var filesData = fileProcessor.Process(formFiles);
            var content = new Content(filesData, MinioOptions.PHOTO_BUCKET);

            var command = new PetAddPhotosCommand(id, petId, content);

            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            var result = await handler.Execute(command, cancellationToken);

            return result.ToResponse();
        }
    }
}
