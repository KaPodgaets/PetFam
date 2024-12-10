using PetFam.Shared.Abstractions;

namespace PetFam.VolunteeringApplications.Application.Queries.Get;

public record GetWithPaginationQuery(
    Guid? UserId,
    Guid? AdminId,
    int? Status,
    int PageNumber,
    int PageSize) : IQuery;