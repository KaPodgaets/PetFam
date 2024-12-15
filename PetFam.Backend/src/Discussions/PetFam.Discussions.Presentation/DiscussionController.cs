﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetFam.Discussions.Application.Commands.Close;
using PetFam.Discussions.Application.Commands.Create;
using PetFam.Discussions.Domain;
using PetFam.Discussions.Presentation.Requests;
using PetFam.Framework;
using PetFam.Framework.Authorization;
using PetFam.Shared.Models;

namespace PetFam.Discussions.Presentation;

public class DiscussionController:ApplicationController
{
    public DiscussionController(ILogger<ApplicationController> logger) : base(logger)
    {
    }
    
    [Permission(Permissions.Discussions.Read)]
    [HttpGet]
    public async Task<ActionResult<PagedList<Discussion>>> GetAllVolunteers(
        [FromServices] _ handler,
        [FromQuery] _ request,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();

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
}

