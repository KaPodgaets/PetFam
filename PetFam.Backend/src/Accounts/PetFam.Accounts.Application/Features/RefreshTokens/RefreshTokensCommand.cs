using PetFam.Shared.Abstractions;

namespace PetFam.Accounts.Application.Features.RefreshTokens;

public record RefreshTokensCommand(
    Guid RefreshToken):ICommand;