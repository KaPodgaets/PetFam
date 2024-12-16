using PetFam.Discussions.Domain;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Discussions.Application;

public interface IDiscussionsRepository
{
    Task<Result<Guid>> Add(Discussion model, CancellationToken cancellationToken = default);
    Task<Result<Discussion>> GetById(DiscussionId id, CancellationToken cancellationToken = default);
    Result<Guid> Update(Discussion volunteer, CancellationToken cancellationToken = default);
    Result<Guid> Delete(Discussion volunteer, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<Discussion>>> GetAllAsync(CancellationToken cancellationToken = default);
}