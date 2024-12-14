using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public async Task<ActionResult<PagedList<Discussion>>> Create(
        [FromServices] _ handler,
        [FromBody] CreateDiscussionRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToCommand();

        var result = await handler.HandleAsync(query, cancellationToken);

        return result.ToResponse();
    }
}

