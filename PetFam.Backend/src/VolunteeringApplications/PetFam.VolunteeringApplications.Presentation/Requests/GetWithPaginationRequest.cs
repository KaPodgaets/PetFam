using PetFam.VolunteeringApplications.Application.Queries.Get;

namespace PetFam.VolunteeringApplications.Presentation.Requests;

public record GetWithPaginationRequest(
    Guid? UserId,
    Guid? AdminId,
    int? Status,
    int PageNumber,
    int PageSize)
{
    public GetWithPaginationQuery ToQuery()
    {
        return new GetWithPaginationQuery(
            UserId,
            AdminId,
            Status,
            PageNumber,
            PageSize);
    }
}