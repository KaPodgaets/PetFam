using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Contracts;
using PetFam.Api.Extensions;
using PetFam.Api.Response;
using PetFam.Application.FileProvider;
using PetFam.Application.VolunteerManagement.Create;
using PetFam.Application.VolunteerManagement.Delete;
using PetFam.Application.VolunteerManagement.PetManagement.AddPhotos;
using PetFam.Application.VolunteerManagement.PetManagement.Create;
using PetFam.Application.VolunteerManagement.UpdateMainInfo;
using PetFam.Application.VolunteerManagement.UpdateRequisites;
using PetFam.Application.VolunteerManagement.UpdateSocialMedia;
using PetFam.Domain.Shared;
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

        [HttpPut("{id:guid}/requisites")]
        public async Task<ActionResult<Guid>> UpdateRequisites(
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

        [HttpPut("{id:guid}/add-pet")]
        public async Task<ActionResult<Guid>> AddNewPet(
            [FromRoute] Guid id,
            [FromServices] CreatePetHandler handler,
            [FromServices] IValidator<CreatePetRequest> validator,
            [FromBody] CreatePetDto dto,
            CancellationToken cancellationToken = default)
        {
            var request = new CreatePetRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            var result = await handler.Handle(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpPut("{id:guid}/add-photos/{petId:guid}")]
        public async Task<ActionResult<string>> AddNewPet(
            [FromRoute] Guid id,
            [FromRoute] Guid petId,
            [FromServices] PetAddPhotosHandler handler,
            [FromServices] IValidator<PetAddPhotosCommand> validator,
            [FromForm] IFormFileCollection formFiles,
            CancellationToken cancellationToken = default)
        {
            var filesData = new List<FileData>();
            try
            {
                foreach (var item in formFiles)
                {
                    var extension = Path.GetExtension(item.FileName);
                    var fileMetadata = new FileMetedata(MinioOptions.PHOTO_BUCKET, Guid.NewGuid().ToString() + extension);

                    var stream = item.OpenReadStream();
                    var fileData = new FileData(stream, fileMetadata);

                    filesData.Add(fileData);
                }

                var content = new Content(filesData, MinioOptions.PHOTO_BUCKET);

                var command = new PetAddPhotosCommand(id, petId, content);

                var validationResult = await validator.ValidateAsync(command, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return validationResult.ToResponse();
                }

                var result = await handler.Execute(command, cancellationToken);

                return result.ToResponse();
            } finally {

                foreach( var file in filesData)
                {
                    await file.Stream.DisposeAsync();
                }
            }
        }
    }
}
