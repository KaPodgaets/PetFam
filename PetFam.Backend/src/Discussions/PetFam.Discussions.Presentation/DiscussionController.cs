using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetFam.Discussions.Application.Commands.AddMessage;
using PetFam.Discussions.Application.Commands.Close;
using PetFam.Discussions.Application.Commands.Create;
using PetFam.Discussions.Application.Commands.DeleteMessage;
using PetFam.Discussions.Application.Commands.EditeMessage;
using PetFam.Discussions.Application.Queries.GetById;
using PetFam.Discussions.Domain;
using PetFam.Discussions.Presentation.Requests;
using PetFam.Framework;
using PetFam.Framework.Authorization;

namespace PetFam.Discussions.Presentation;

public class DiscussionController:ApplicationController
{
    public DiscussionController(ILogger<ApplicationController> logger) : base(logger)
    {
    }
    
    [Permission(Permissions.Discussions.Read)]
    [HttpGet]
    public async Task<ActionResult<Discussion>> GetDiscussionById(
        [FromServices] GetByIdHandler handler,
        [FromQuery] Guid discussionId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetByIdQuery(discussionId);

        var result = await handler.HandleAsync(query, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.Discussions.Create)]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateDiscussionHandler handler,
        [FromBody] CreateDiscussionRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();

        var result = await handler.ExecuteAsync(command, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.Discussions.Update)]
    [HttpPut("{id:guid}/closed")]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CloseDiscussionHandler handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new CloseDiscussionCommand(id);

        var result = await handler.ExecuteAsync(command, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.Discussions.Create)]
    [HttpPost("{id:guid}/new-message")]
    public async Task<ActionResult<Guid>> AddMessage(
        [FromServices] AddMessageHandler handler,
        [FromRoute] Guid id,
        [FromBody] AddMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);

        var result = await handler.ExecuteAsync(command, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.Discussions.Delete)]
    [HttpDelete("{discussionId:guid}/message/{messageId:guid}")]
    public async Task<ActionResult<Guid>> DeleteMessage(
        [FromServices] DeleteMessageHandler handler,
        [FromRoute] Guid discussionId,
        [FromRoute] Guid messageId,
        [FromBody] Guid userId,
        CancellationToken cancellationToken = default)
    {
        // TODO: add getting userId from claims in JWT
        
        var command = new DeleteMessageCommand(discussionId, messageId, userId);

        var result = await handler.ExecuteAsync(command, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission(Permissions.Discussions.Update)]
    [HttpPut("{id:guid}/message/{messageId:guid}")]
    public async Task<ActionResult<Guid>> EditMessage(
        [FromServices] EditMessageHandler handler,
        [FromRoute] Guid id,
        [FromRoute] Guid messageId,
        [FromBody] EditMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new EditMessageCommand(id, messageId, request.UserId, request.NewText);

        var result = await handler.ExecuteAsync(command, cancellationToken);

        return result.ToResponse();
    }
}