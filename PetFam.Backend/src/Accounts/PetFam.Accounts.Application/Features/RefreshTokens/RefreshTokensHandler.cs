using System.Security.Claims;
using Microsoft.Extensions.Logging;
using PetFam.Accounts.Application.Database;
using PetFam.Accounts.Application.DataModels;
using PetFam.Accounts.Application.Interfaces;
using PetFam.Accounts.Contracts.Responses;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Models;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Application.Features.RefreshTokens;

public class RefreshTokensHandler
    :ICommandHandler<LoginResultDataModel, RefreshTokensCommand>
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

    public async Task<Result<LoginResultDataModel>> ExecuteAsync(
        RefreshTokensCommand command,
        CancellationToken cancellationToken = default)
    {
        var sessionResult = await _refreshSessionManager
            .GetByRefreshToken(command.RefreshToken, cancellationToken);
        if (sessionResult.IsFailure)
            return sessionResult.Errors;

        if (sessionResult.Value.ExpiresIn < DateTime.UtcNow)
            return Errors.Tokens.ExpiredToken().ToErrorList();
        
        await _refreshSessionManager.DeleteById(sessionResult.Value, cancellationToken);
        
        var jwtResult = await _tokenProvider
            .GenerateAccessToken(sessionResult.Value.User, cancellationToken);
        var newAccessToken = jwtResult.AccessToken;
        var newRefreshToken = await _tokenProvider.GenerateRefreshToken(
            sessionResult.Value.User,
            jwtResult.AccessTokenJti,
            cancellationToken);
        
        return new LoginResultDataModel(newAccessToken, newRefreshToken);
    }
}