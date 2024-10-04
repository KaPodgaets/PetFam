using PetFam.Domain.Shared;

namespace PetFam.Application.Volunteers.Create
{
    public interface ICreateVolunteerHandler
    {
        Task<Result<Guid>> Handle(CreateVolunteerRequest request, CancellationToken cancellationToken);
    }
}