using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFam.Accounts.Application.Database;
using PetFam.Accounts.Domain;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Application.Features.RegisterUser;

public class RegisterUserHandler
    :ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RegisterUserHandler> _logger;
    private readonly IParticipantAccountsManager _participantAccountsManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly RoleManager<Role> _roleManager;

    public RegisterUserHandler(
        UserManager<User> userManager,
        ILogger<RegisterUserHandler> logger,
        IParticipantAccountsManager participantAccountsManager, 
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork, 
        RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _logger = logger;
        _participantAccountsManager = participantAccountsManager;
        _unitOfWork = unitOfWork;
        _roleManager = roleManager;
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
        var participantRole = await _roleManager.FindByNameAsync(ParticipantAccount.RoleName)
                        ?? throw new ApplicationException("Could not find admin role.");
        
        var user = User.CreateParticipant(command.Email, [participantRole]);
        
        var participantAccount = new ParticipantAccount
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            FullName = user.UserName ?? string.Empty,
        };
        
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var result = await _userManager.CreateAsync(user, command.Password);
            
            if (result.Succeeded is false)
            {
                var errors = result.Errors
                    .Select(e => Error.Failure(e.Code,e.Description))
                    .ToList();
                
                transaction.Rollback();
                return new ErrorList(errors);
            }

            await _participantAccountsManager.CreateAccount(participantAccount, cancellationToken);
            
            transaction.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError("error while registering new participant. Error - {error}", e.Message);
            transaction.Rollback();
            return Errors.General.Failure().ToErrorList();
        }
        _logger.LogInformation("New user created {email}", user.Email);
        return Result.Success();
    }
}