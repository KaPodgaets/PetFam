using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetFam.Accounts.Application.Interfaces;
using PetFam.Accounts.Domain;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Application.Login;

public class LoginHandler
    : ICommandHandler<string, LoginCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(
        ILogger<LoginHandler> logger,
        UserManager<User> userManager,
        ITokenProvider tokenProvider)
    {
        _logger = logger;
        _userManager = userManager;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<string>> ExecuteAsync(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user is null)
            return Errors.User.InvalidCredentials().ToErrorList();

        var passwordCheck = await _userManager.CheckPasswordAsync(user, command.Password);
        if (passwordCheck is false)
            return Errors.User.InvalidCredentials().ToErrorList();
        
        var token = _tokenProvider.GetAccessToken(user);

        return token;
    }
}