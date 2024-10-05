using PetFam.Domain.Shared;

namespace PetFam.Application.Volunteers.UpdateSocialMedia
{
    public interface IUpdateSocialMediaHandler
    {
        Task<Result<Guid>> Handle(UpdateSocialMediaRequest request, CancellationToken cancellationToken = default);
    }
}