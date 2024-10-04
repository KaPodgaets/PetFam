using PetFam.Domain.Shared;

namespace PetFam.Application.Volunteers.UpdateName
{
    public interface IVolunteerUpdateMainInfoHandler
    {
        Task<Result<Guid>> Handle(UpdateMainInfoRequest request, CancellationToken cancellationToken = default);
    }
}