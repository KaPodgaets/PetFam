using PetFam.Domain.Shared;

namespace PetFam.Application.Volunteers.UpdateRequisites
{
    public interface IUpdateRequisitesHandler
    {
        Task<Result<Guid>> Handle(UpdateRequisitesRequest request, CancellationToken cancellationToken = default);
    }
}