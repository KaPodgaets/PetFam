using Microsoft.AspNetCore.Identity;
using PetFam.Accounts.Domain;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Application;

public class RegisterUserHandler
    :ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;

    public RegisterUserHandler(
        UserManager<User> userManager)
    {
        _userManager = userManager;
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
        
        return Result.Success();
    }
}