using System.Security.Claims;
using PetFam.Accounts.Application.Models;
using PetFam.Accounts.Domain;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Application.Interfaces;

public interface ITokenProvider
{
    Task<JwtResult> GenerateAccessToken(User user, CancellationToken cancellationToken = default);
    Task<Guid> GenerateRefreshToken(User user, Guid jti, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<Claim>>> GetUserClaims(string token,CancellationToken cancellationToken = default);
}