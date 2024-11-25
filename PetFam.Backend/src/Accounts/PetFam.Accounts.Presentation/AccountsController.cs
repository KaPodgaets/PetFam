using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetFam.Accounts.Application.Login;
using PetFam.Accounts.Application.RefreshTokens;
using PetFam.Accounts.Application.RegisterUser;
using PetFam.Accounts.Contracts.Requests;
using PetFam.Accounts.Presentation.Providers;
using PetFam.Framework;

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
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.UserEmail, request.Password);
        var result = await handler.ExecuteAsync(command, cancellationToken);
        
        var setCookieResult = httpContextProvider.SetRefreshSessionCookie(result.Value.RefreshToken);
        if(setCookieResult.IsFailure)
            return setCookieResult.ToResponse();
        
        return Ok(result.Value.AccessToken);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokens(
        [FromHeader] string accessToken,
        [FromServices] RefreshTokensHandler handler,
        CancellationToken cancellationToken)
    {
        var getRefreshSessionCookieResult = httpContextProvider.GetRefreshSessionCookie();
        if (getRefreshSessionCookieResult.IsFailure)
        {
            return Unauthorized();
        }
        
        var command = new RefreshTokensCommand(accessToken, getRefreshSessionCookieResult.Value);
        var refreshResult = await handler.ExecuteAsync(command, cancellationToken);
        if(refreshResult.IsFailure)
            return refreshResult.Errors.ToResponse();
        
        var setCookieResult = httpContextProvider.SetRefreshSessionCookie(refreshResult.Value.RefreshToken);
        if(setCookieResult.IsFailure)
            return setCookieResult.ToResponse();
        
        return Ok(refreshResult.Value.AccessToken);
    }
}