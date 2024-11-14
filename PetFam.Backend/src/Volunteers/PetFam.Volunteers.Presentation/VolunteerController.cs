using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetFam.Framework;
using PetFam.Shared.Dtos;
using PetFam.Shared.Models;
using PetFam.Volunteers.Application.VolunteerManagement.Commands.AddPetPhotos;
using PetFam.Volunteers.Application.VolunteerManagement.Commands.ChangePetMainPhoto;
using PetFam.Volunteers.Application.VolunteerManagement.Commands.Create;
using PetFam.Volunteers.Application.VolunteerManagement.Commands.CreatePet;
using PetFam.Volunteers.Application.VolunteerManagement.Commands.Delete;
using PetFam.Volunteers.Application.VolunteerManagement.Commands.DeletePet;
using PetFam.Volunteers.Application.VolunteerManagement.Commands.DeletePetPhotos;
using PetFam.Volunteers.Application.VolunteerManagement.Commands.PetStatusUpdate;
using PetFam.Volunteers.Application.VolunteerManagement.Commands.PetUpdate;
using PetFam.Volunteers.Application.VolunteerManagement.Commands.UpdateMainInfo;
using PetFam.Volunteers.Application.VolunteerManagement.Commands.UpdateRequisites;
using PetFam.Volunteers.Application.VolunteerManagement.Commands.UpdateSocialMedia;
using PetFam.Volunteers.Application.VolunteerManagement.Queries.GetAllVolunteers;
using PetFam.Volunteers.Contracts.Volunteer;

namespace PetFam.Volunteers.Presentation
{
    public class VolunteerController : ApplicationController
    {
        public VolunteerController(ILogger<VolunteerController> logger)
            : base(logger)
        {
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<VolunteerDto>>> GetAllVolunteers(
            [FromServices] GetVolunteersWithPaginationHandler handler,
            [FromQuery] GetVolunteersWithPaginationRequest request,
            CancellationToken cancellationToken = default)
        {
            var query = request.ToQuery();

            var result = await handler.HandleAsync(query, cancellationToken);

            return result.ToResponse();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.ExecuteAsync(request.ToCommand(), cancellationToken);

            return result.ToResponse();
        }

        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult<Guid>> UpdateMainInfo(
            [FromRoute] Guid id,
            [FromServices] UpdateMainInfoHandler handler,
            [FromBody] UpdateMainInfoRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(id);

            var result = await handler.ExecuteAsync(command, cancellationToken);

            return result.ToResponse();
        }

        [HttpPut("{id:guid}/requisites")]
        public async Task<ActionResult<Guid>> UpdateRequisites(
            [FromRoute] Guid id,
            [FromServices] UpdateRequisitesHandler handler,
            [FromBody] UpdateRequisitesRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = new UpdateRequisitesCommand(id, request.Requisites);

            var result = await handler.ExecuteAsync(command, cancellationToken);

            return result.ToResponse();
        }

        [HttpPut("{id:guid}/social-media")]
        public async Task<ActionResult<Guid>> UpdateSocialMedia(
            [FromRoute] Guid id,
            [FromServices] UpdateSocialMediaHandler handler,
            [FromBody] UpdateSocialMediaRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = new UpdateSocialMediaCommand(id, request.SocialMediaLinks);

            var result = await handler.ExecuteAsync(command, cancellationToken);

            return result.ToResponse();
        }

        [HttpDelete("{id:guid}/delete")]
        public async Task<ActionResult<Guid>> Delete(
            [FromRoute] Guid id,
            [FromServices] DeleteHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteCommand(id);

            var result = await handler.ExecuteAsync(command, cancellationToken);

            return result.ToResponse();
        }

        [HttpPost("{id:guid}/add-pet")]
        public async Task<ActionResult<Guid>> AddNewPet(
            [FromRoute] Guid id,
            [FromServices] CreatePetHandler handler,
            [FromBody] CreatePetRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(id);

            var result = await handler.ExecuteAsync(command, cancellationToken);

            return result.ToResponse();
        }

        [HttpDelete("{id:guid}/pet/{petId:guid}")]
        public async Task<ActionResult<Guid>> DeletePet(
            [FromRoute] Guid id,
            [FromRoute] Guid petId,
            [FromServices] DeletePetHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new DeletePetCommand(id, petId);

            var result = await handler.ExecuteAsync(command, cancellationToken);

            return result.ToResponse();
        }

        // [HttpPost("{id:guid}/pet/{petId:guid}/photos")]
        // public async Task<ActionResult<string>> AddPetPhotos(
        //     [FromRoute] Guid id,
        //     [FromRoute] Guid petId,
        //     [FromServices] PetAddPhotosHandler handler,
        //     [FromForm] IFormFileCollection formFiles,
        //     CancellationToken cancellationToken = default)
        // {
        //     await using var fileProcessor = new FormFileProcessor();
        //     var filesData = fileProcessor.Process(formFiles);
        //     var content = new Content(filesData, Constants.FileManagementOptions.PHOTO_BUCKET);
        //
        //     var command = new PetAddPhotosCommand(id, petId, content);
        //
        //     var result = await handler.ExecuteAsync(command, cancellationToken);
        //
        //     return result.ToResponse();
        // }

        [HttpDelete("{id:guid}/photos/{petId:guid}")]
        public async Task<ActionResult<string[]>> AddPetPhotos(
            [FromRoute] Guid id,
            [FromRoute] Guid petId,
            [FromBody] string[] photos,
            [FromServices] DeletePetPhotosHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new DeletePetPhotosCommand(id, petId, photos);

            var result = await handler.ExecuteAsync(command, cancellationToken);

            return result.ToResponse();
        }

        [HttpPut("{id:guid}/pet/{petId:guid}")]
        public async Task<ActionResult<Guid>> UpdatePet(
            [FromRoute] Guid id,
            [FromRoute] Guid petId,
            [FromBody] PetUpdateRequest request,
            [FromServices] PetUpdateHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(id, petId);

            var result = await handler.ExecuteAsync(command, cancellationToken);

            return result.ToResponse();
        }

        [HttpPut("{id:guid}/pet-status/{petId:guid}")]
        public async Task<ActionResult<Guid>> UpdatePetStatus(
            [FromRoute] Guid id,
            [FromRoute] Guid petId,
            [FromQuery] int newPetStatus,
            [FromServices] PetStatusUpdateHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new PetStatusUpdateCommand(id, petId, newPetStatus);
            
            var result = await handler.ExecuteAsync(command, cancellationToken);
            
            return result.ToResponse();
        }

        [HttpPut("{id:guid}/pet/{petId:guid}/main-photo")]
        public async Task<ActionResult<string>> SetMainPhoto(
            [FromRoute] Guid id,
            [FromRoute] Guid petId,
            [FromQuery] string photoPath,
            [FromServices] ChangePetMainPhotoHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new ChangePetMainPhotoCommand(id, petId, photoPath);
            
            var result = await handler.ExecuteAsync(command, cancellationToken);
            
            var actionResult = result.ToResponse();
            return actionResult;
        }
    }
}