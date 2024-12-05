using PetFam.Shared.Abstractions;

namespace PetFam.Accounts.Application.Features.RefreshTokens;

public record RefreshTokensCommand(
    string AccessToken,
    Guid RefreshToken):ICommand;