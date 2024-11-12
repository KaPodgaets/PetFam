using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Extensions;
using PetFam.Api.Processors;
using PetFam.Api.Requests.Volunteer;
using PetFam.Application;
using PetFam.Application.Dtos;
using PetFam.Application.FileProvider;
using PetFam.Application.VolunteerManagement.Commands.Create;
using PetFam.Application.VolunteerManagement.Commands.Delete;
using PetFam.Application.VolunteerManagement.Commands.UpdateMainInfo;
using PetFam.Application.VolunteerManagement.Commands.UpdateRequisites;
using PetFam.Application.VolunteerManagement.Commands.UpdateSocialMedia;
using PetFam.Application.VolunteerManagement.PetManagement.AddPetPhotos;
using PetFam.Application.VolunteerManagement.PetManagement.CreatePet;
using PetFam.Application.VolunteerManagement.Queries.GetAllVolunteers;
using PetFam.Infrastructure.Options;

namespace PetFam.Api.Controllers
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
            [FromServices] IUpdateMainInfoHandler handler,
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
            [FromServices] IUpdateRequisitesHandler handler,
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
            [FromServices] IUpdateSocialMediaHandler handler,
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
            [FromServices] IDeleteHandler handler,
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

        [HttpPost("{id:guid}/add-photos/{petId:guid}")]
        public async Task<ActionResult<string>> AddPetPhotos(
            [FromRoute] Guid id,
            [FromRoute] Guid petId,
            [FromServices] PetAddPhotosHandler handler,
            [FromForm] IFormFileCollection formFiles,
            CancellationToken cancellationToken = default)
        {
            
            await using var fileProcessor = new FormFileProcessor();
            var filesData = fileProcessor.Process(formFiles);
            var content = new Content(filesData, MinioOptions.PHOTO_BUCKET);

            var command = new PetAddPhotosCommand(id, petId, content);

            var result = await handler.ExecuteAsync(command, cancellationToken);

            return result.ToResponse();
        }
    }
}
