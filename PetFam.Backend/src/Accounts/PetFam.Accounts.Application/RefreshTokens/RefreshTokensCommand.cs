using PetFam.Shared.Abstractions;

namespace PetFam.Accounts.Application.RefreshTokens;

public record RefreshTokensCommand(
    string AccessToken,
    Guid RefreshToken):ICommand;