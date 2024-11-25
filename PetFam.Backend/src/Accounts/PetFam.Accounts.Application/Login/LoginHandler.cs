using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetFam.Accounts.Application.Interfaces;
using PetFam.Accounts.Contracts.Responses;
using PetFam.Accounts.Domain;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Application.Login;

public class LoginHandler
    : ICommandHandler<LoginResponse, LoginCommand>
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

    public async Task<Result<LoginResponse>> ExecuteAsync(
        LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user is null)
            return Errors.User.InvalidCredentials().ToErrorList();

        var passwordCheck = await _userManager.CheckPasswordAsync(user, command.Password);
        if (passwordCheck is false)
            return Errors.User.InvalidCredentials().ToErrorList();
        
        var jwtResult = await _tokenProvider.GenerateAccessToken(user, cancellationToken);
        var accessToken = jwtResult.AccessToken;
        var refreshToken = await _tokenProvider.GenerateRefreshToken(user, jwtResult.AccessTokenJti, cancellationToken);
        
        var response = new LoginResponse(accessToken, refreshToken);
        _logger.LogInformation("user {userId} logged in", user.Id);
        return response;
    }
}