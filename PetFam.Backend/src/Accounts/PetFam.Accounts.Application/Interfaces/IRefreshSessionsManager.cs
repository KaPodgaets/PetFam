using PetFam.Accounts.Domain;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Application.Interfaces;

public interface IRefreshSessionsManager
{
    Task<Result<RefreshSession>> GetByRefreshToken(Guid refreshToken, CancellationToken cancellationToken = default);
    Task DeleteById(RefreshSession session, CancellationToken cancellationToken = default);
    Task Create(RefreshSession session, CancellationToken cancellationToken = default);
}