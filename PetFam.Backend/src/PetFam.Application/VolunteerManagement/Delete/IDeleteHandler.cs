using PetFam.Domain.Shared;

namespace PetFam.Application.VolunteerManagement.Delete
{
    public interface IDeleteHandler
    {
        Task<Result<Guid>> Handle(DeleteRequest request, CancellationToken cancellationToken = default);
    }
}