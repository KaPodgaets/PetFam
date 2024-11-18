using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetFam.Accounts.Application.Login;
using PetFam.Accounts.Application.RegisterUser;
using PetFam.Accounts.Presentation.Requests;
using PetFam.Framework;

namespace PetFam.Accounts.Presentation;

public class AccountsController(ILogger<ApplicationController> logger) : ApplicationController(logger)
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
    public async Task<ActionResult<string>> Login(
        [FromBody] LoginRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.UserEmail, request.Password);
        var result = await handler.ExecuteAsync(command, cancellationToken);
         
        return result.ToResponse();
    }
}