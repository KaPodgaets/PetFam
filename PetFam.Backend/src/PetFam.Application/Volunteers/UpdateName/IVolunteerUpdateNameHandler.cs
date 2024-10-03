using PetFam.Domain.Shared;

namespace PetFam.Application.Volunteers.UpdateName
{
    public interface IVolunteerUpdateNameHandler
    {
        Task<Result<Guid>> Execute(VolunteerUpdateNameRequest request, CancellationToken cancellationToken = default);
    }
}