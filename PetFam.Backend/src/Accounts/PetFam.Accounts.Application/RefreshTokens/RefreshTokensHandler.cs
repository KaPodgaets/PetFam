using System.Security.Claims;
using Microsoft.Extensions.Logging;
using PetFam.Accounts.Application.Interfaces;
using PetFam.Accounts.Contracts.Responses;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Models;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Application.RefreshTokens;

public class RefreshTokensHandler
    :ICommandHandler<LoginResponse, RefreshTokensCommand>
{
    private readonly ILogger<RefreshTokensHandler> _logger;
    private readonly IRefreshSessionsManager _refreshSessionManager;
    private readonly ITokenProvider _tokenProvider;

    public RefreshTokensHandler(
        ILogger<RefreshTokensHandler> logger,
        IRefreshSessionsManager refreshSessionManager,
        ITokenProvider tokenProvider)
    {
        _logger = logger;
        _refreshSessionManager = refreshSessionManager;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<LoginResponse>> ExecuteAsync(
        RefreshTokensCommand command,
        CancellationToken cancellationToken = default)
    {
        var sessionResult = await _refreshSessionManager
            .GetByRefreshToken(command.RefreshToken, cancellationToken);
        if (sessionResult.IsFailure)
            return sessionResult.Errors;

        if (sessionResult.Value.ExpiresIn < DateTime.UtcNow)
            return Errors.Tokens.ExpiredToken().ToErrorList();
        
        var userClaimsResult = await _tokenProvider.GetUserClaims(command.AccessToken, cancellationToken);
        if(userClaimsResult.IsFailure)
            return userClaimsResult.Errors;
        
        var userIdString = userClaimsResult.Value
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if(!Guid.TryParse(userIdString, out var userId))
            return Errors.General.Failure().ToErrorList();

        if(sessionResult.Value.UserId != userId)
            return Errors.Tokens.NotValid().ToErrorList();
        
        var userJtiString = userClaimsResult.Value
            .FirstOrDefault(c => c.Type == CustomClaims.Jti)?.Value;
        if(!Guid.TryParse(userJtiString, out var userJti))
            return Errors.General.Failure().ToErrorList();
        
        if(sessionResult.Value.Jti != userJti)
            return Errors.Tokens.NotValid().ToErrorList();
        
        await _refreshSessionManager.DeleteById(sessionResult.Value, cancellationToken);
        
        var jwtResult = await _tokenProvider
            .GenerateAccessToken(sessionResult.Value.User, cancellationToken);
        var newAccessToken = jwtResult.AccessToken;
        var newRefreshToken = await _tokenProvider.GenerateRefreshToken(
            sessionResult.Value.User,
            jwtResult.AccessTokenJti,
            cancellationToken);
        
        return new LoginResponse(newAccessToken, newRefreshToken);
    }
}