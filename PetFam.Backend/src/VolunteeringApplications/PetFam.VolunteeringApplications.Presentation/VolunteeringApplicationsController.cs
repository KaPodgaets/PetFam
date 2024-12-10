using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetFam.Framework;
using PetFam.Framework.Authorization;
using PetFam.Shared.Models;
using PetFam.VolunteeringApplications.Application.Commands.Approve;
using PetFam.VolunteeringApplications.Application.Commands.AssignAdmin;
using PetFam.VolunteeringApplications.Application.Commands.CreateApplication;
using PetFam.VolunteeringApplications.Application.Commands.Reject;
using PetFam.VolunteeringApplications.Application.Commands.RequestRevision;
using PetFam.VolunteeringApplications.Application.Commands.Shared;
using PetFam.VolunteeringApplications.Application.Commands.StartReview;
using PetFam.VolunteeringApplications.Application.Commands.UnassignAdmin;
using PetFam.VolunteeringApplications.Application.Commands.Update;
using PetFam.VolunteeringApplications.Application.Queries.Get;
using PetFam.VolunteeringApplications.Application.Queries.GetById;
using PetFam.VolunteeringApplications.Domain;
using PetFam.VolunteeringApplications.Presentation.Requests;

namespace PetFam.VolunteeringApplications.Presentation;

public class VolunteeringApplicationsController:ApplicationController
{
    public VolunteeringApplicationsController(ILogger<ApplicationController> logger) : base(logger)
    {
    }
    
    [Permission(Permissions.VolunteeringApplications.Read)]
    [HttpGet]
    public async Task<ActionResult<PagedList<VolunteeringApplication>>> GetPaginated(
        [FromServices] GetHandler handler,
        [FromQuery] GetWithPaginationRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();

        var result = await handler.HandleAsync(query, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.VolunteeringApplications.Read)]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VolunteeringApplication>> GetById(
        [FromServices] GetByIdHandler handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetByIdQuery(id);

        var result = await handler.HandleAsync(query, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.VolunteeringApplications.Create)]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateApplicationHandler handler,
        [FromBody] CreateApplicationRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.ExecuteAsync(request.ToCommand(), cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.VolunteeringApplications.Update)]
    [HttpPut("{id:guid}/admin")]
    public async Task<ActionResult<Guid>> AssignAdmin(
        [FromServices] AssignAdminHandler handler,
        [FromRoute] Guid id,
        [FromBody]  Guid adminId,
        CancellationToken cancellationToken = default)
    {
        var command = new AssignAdminCommand(id, adminId);
        var result = await handler.ExecuteAsync(command, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.VolunteeringApplications.Update)]
    [HttpDelete("{id:guid}/admin")]
    public async Task<ActionResult<Guid>> UnassignAdmin(
        [FromServices] UnassignAdminHandler handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new UnassignAdminCommand(id);
        var result = await handler.ExecuteAsync(command, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.VolunteeringApplications.Update)]
    [HttpPut("{id:guid}/volunteer-info")]
    public async Task<ActionResult<Guid>> AssignAdmin(
        [FromServices] UpdateHandler handler,
        [FromRoute] Guid id,
        [FromBody]  string volunteerInfo,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateCommand(id, volunteerInfo);
        var result = await handler.ExecuteAsync(command, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.VolunteeringApplications.Update)]
    [HttpPut("{id:guid}/approve")]
    public async Task<ActionResult<Guid>> Approve(
        [FromServices] ApproveHandler handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new ChangeApplicationStatusCommand(id);
        var result = await handler.ExecuteAsync(command, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.VolunteeringApplications.Update)]
    [HttpPut("{id:guid}/reject")]
    public async Task<ActionResult<Guid>> Reject(
        [FromServices] RejectHandler handler,
        [FromRoute] Guid id,
        [FromBody] string comment,
        CancellationToken cancellationToken = default)
    {
        var command = new RejectApplicationCommand(id, comment);
        var result = await handler.ExecuteAsync(command, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.VolunteeringApplications.Update)]
    [HttpPut("{id:guid}/revision")]
    public async Task<ActionResult<Guid>> RequestRevision(
        [FromServices] RequestRevisionHandler handler,
        [FromRoute] Guid id,
        [FromBody] string comment,
        CancellationToken cancellationToken = default)
    {
        var command = new RejectApplicationCommand(id, comment);
        var result = await handler.ExecuteAsync(command, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.VolunteeringApplications.Update)]
    [HttpPut("{id:guid}/review")]
    public async Task<ActionResult<Guid>> StartReview(
        [FromServices] StartReviewHandler handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new ChangeApplicationStatusCommand(id);
        var result = await handler.ExecuteAsync(command, cancellationToken);

        return result.ToResponse();
    }
}