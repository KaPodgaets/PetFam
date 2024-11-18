using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetFam.Accounts.Domain;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Application.RegisterUser;

public class RegisterUserHandler
    :ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RegisterUserHandler> _logger;

    public RegisterUserHandler(
        UserManager<User> userManager,
        ILogger<RegisterUserHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    public async Task<Result> ExecuteAsync(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        //check email is unique
        var existingUser = await _userManager.FindByEmailAsync(command.Email);
        if (existingUser != null)
            return Errors.General.AlreadyExist("email").ToErrorList();
        
        // create new user in bd
        
        var user = new User
        {
            Email = command.Email,
            UserName = command.Email,
        };
        
        var result = await _userManager.CreateAsync(user, command.Password);
        
        if (result.Succeeded is false)
        {
             var errors = result.Errors
                 .Select(e => Error.Failure(e.Code,e.Description))
                 .ToList();
             
             return new ErrorList(errors);
        }
        
        _logger.LogInformation("New user created {email}", user.Email);
        return Result.Success();
    }
}