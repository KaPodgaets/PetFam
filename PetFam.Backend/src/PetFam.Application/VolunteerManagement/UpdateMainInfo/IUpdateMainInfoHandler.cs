using PetFam.Domain.Shared;

namespace PetFam.Application.VolunteerManagement.UpdateMainInfo
{
    public interface IUpdateMainInfoHandler
    {
        Task<Result<Guid>> Handle(UpdateMainInfoRequest request, CancellationToken cancellationToken = default);
    }
}