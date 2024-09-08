using PetFam.Domain.Shared;

namespace PetFam.Application.Volunteers.Create
{
    public interface ICreateVolunteerHandler
    {
        Task<Result<Guid>> Execute(CreateVolunteerRequest request, CancellationToken cancellationToken);
    }
}