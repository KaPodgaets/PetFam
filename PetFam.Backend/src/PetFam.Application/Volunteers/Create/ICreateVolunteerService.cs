using PetFam.Domain.Shared;

namespace PetFam.Application.Volunteers.Create
{
    public interface ICreateVolunteerService
    {
        Task<Result<Guid>> Execute(CreateVolunteerRequest request, CancellationToken cancellationToken);
    }
}