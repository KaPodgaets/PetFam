using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetFam.Accounts.Application.DataModels;
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
        if (result.IsFailure)
            return result.Errors.ToResponse<LoginResponse>();

        var response = result.Value.ToResponse();

        HttpContext.Response.Cookies.Append("refreshToken", result.Value.RefreshToken.ToString());
        return response.ToResponse();
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<LoginResponse>> RefreshTokens(
        [FromServices] RefreshTokensHandler handler,
        CancellationToken cancellationToken)
    {
        var tokenResult = HttpContext.Request.Cookies.TryGetValue("refreshToken", out var value);

        if (tokenResult is false)
            return Unauthorized();
        if (value is null)
        {
            return Unauthorized();
        }

        var parseGuid = Guid.TryParse(value, out var token);
        if (parseGuid is false)
            return Unauthorized();

        var command = new RefreshTokensCommand(token);
        var refreshResult = await handler.ExecuteAsync(command, cancellationToken);
        if (refreshResult.IsFailure)
            return refreshResult.Errors.ToResponse<LoginResponse>();
        var response = refreshResult.Value.ToResponse();

        HttpContext.Response.Cookies.Append("refreshToken", refreshResult.Value.RefreshToken.ToString());
        return response.ToResponse();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDataModel>> GetUserById(
        [FromRoute] Guid id,
        [FromServices] GetUserByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new GetUserByIdQuery(id);
        var result = await handler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
            return result.Errors.ToResponse<UserDataModel>();

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