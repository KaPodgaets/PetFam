using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetFam.Accounts.Application.Features.GetUserById;
using PetFam.Accounts.Application.Features.Login;
using PetFam.Accounts.Application.Features.RefreshTokens;
using PetFam.Accounts.Application.Features.RegisterUser;
using PetFam.Accounts.Contracts.Requests;
using PetFam.Accounts.Contracts.Responses;
using PetFam.Accounts.Presentation.Providers;
using PetFam.Framework;
using PetFam.Framework.Authorization;

namespace PetFam.Accounts.Presentation;

public class AccountsController(
    ILogger<ApplicationController> logger,
    HttpContextProvider httpContextProvider) : ApplicationController(logger)
{
    [HttpPost("registration")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(request.Email, request.Password);
        var result = await handler.ExecuteAsync(command, cancellationToken);
        
        return result.IsFailure ? result.ToResponse() : Ok();
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.UserEmail, request.Password);
        var result = await handler.ExecuteAsync(command, cancellationToken);
        
        HttpContext.Response.Cookies.Append("refreshToken", result.Value.RefreshToken.ToString());
        return result.ToResponse();
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokens(
        [FromServices] RefreshTokensHandler handler,
        CancellationToken cancellationToken)
    {
        var getRefreshSessionCookieResult = httpContextProvider.GetRefreshSessionCookie();
        if (getRefreshSessionCookieResult.IsFailure)
        {
            return Unauthorized();
        }
        
        var command = new RefreshTokensCommand(getRefreshSessionCookieResult.Value);
        var refreshResult = await handler.ExecuteAsync(command, cancellationToken);
        if(refreshResult.IsFailure)
            return refreshResult.Errors.ToResponse();
        
        HttpContext.Response.Cookies.Append("refreshToken", refreshResult.Value.RefreshToken.ToString());
        
        return Ok(refreshResult.Value.AccessToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(
        [FromRoute] Guid id,
        [FromServices] GetUserByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new GetUserByIdQuery(id);
        var result = await handler.HandleAsync(command, cancellationToken);
        
        if(result.IsFailure)
            return result.Errors.ToResponse();
        
        return Ok(result.Value);
    }
    
    [Permission(Permissions.Accounts.Read)]
    [HttpGet("test")]
    public ActionResult<string?> RegisterUser()
    {
        var result = httpContextProvider.GetAccessToken();
        return result.ToResponse();
    }
}