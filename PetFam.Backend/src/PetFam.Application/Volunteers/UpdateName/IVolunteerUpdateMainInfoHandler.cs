using PetFam.Domain.Shared;

namespace PetFam.Application.Volunteers.UpdateName
{
    public interface IVolunteerUpdateMainInfoHandler
    {
        Task<Result<Guid>> Execute(VolunteerUpdateMainInfoRequest request, CancellationToken cancellationToken = default);
    }
}