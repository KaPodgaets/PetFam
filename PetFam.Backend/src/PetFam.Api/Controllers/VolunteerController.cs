﻿using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Extensions;
using PetFam.Api.Processors;
using PetFam.Api.Requests;
using PetFam.Api.Requests.Volunteer;
using PetFam.Application.FileProvider;
using PetFam.Application.VolunteerManagement.Commands.Create;
using PetFam.Application.VolunteerManagement.Commands.Delete;
using PetFam.Application.VolunteerManagement.Commands.UpdateMainInfo;
using PetFam.Application.VolunteerManagement.Commands.UpdateRequisites;
using PetFam.Application.VolunteerManagement.Commands.UpdateSocialMedia;
using PetFam.Application.VolunteerManagement.PetManagement.AddPhotos;
using PetFam.Application.VolunteerManagement.PetManagement.Create;
using PetFam.Application.VolunteerManagement.Queries.GetAllVolunteers;
using PetFam.Application.VolunteerManagement.Queries.GetPets;
using PetFam.Infrastructure.Options;

namespace PetFam.Api.Controllers
{
    public class VolunteerController : ApplicationController
    {
        public VolunteerController(ILogger<VolunteerController> logger)
            : base(logger)
        {
        }

        [HttpGet("pets-filtered")]
        public async Task<ActionResult> GetFilteredPetsWithPagination(
            [FromQuery] GetFilteredPetsWithPaginationRequest request,
            [FromServices] GetFilteredPetsWithPaginationHandler handler,
            CancellationToken cancellationToken = default)
        {
            var query = request.ToQuery();

            var pagedList = await handler.HandleAsync(query, cancellationToken);

            return Ok(pagedList);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetVolunteerById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            return Ok();
        }


        [HttpGet("/all")]
        public async Task<ActionResult> GetAllVolunteers(
            [FromServices] GetVolunteersWithPaginationHandler handler,
            [FromQuery] GetVolunteersWithPaginationRequest request,
            CancellationToken cancellationToken = default)
        {
            var query = request.ToQuery();

            var pagedList = await handler.HandleAsync(query, cancellationToken);
            
            return Ok(pagedList);
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
