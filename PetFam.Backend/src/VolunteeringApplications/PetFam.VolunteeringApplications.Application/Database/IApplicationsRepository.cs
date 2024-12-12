using PetFam.Shared.SharedKernel.Result;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.Application.Database;

public interface IApplicationsRepository
{
    Task<Result<Guid>> Add(
        VolunteeringApplication model,
        CancellationToken cancellationToken = default);

    Task<Result<VolunteeringApplication>> GetById(
        VolunteeringApplicationId id,
        CancellationToken cancellationToken = default);

    Result<Guid> Update(
        VolunteeringApplication model,
        CancellationToken cancellationToken = default);

    public Result<Guid> Delete(
        VolunteeringApplication model,
        CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyList<VolunteeringApplication>>> GetAllAsync(
        CancellationToken cancellationToken = default);
}