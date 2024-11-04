using PetFam.Domain.Shared;

namespace PetFam.Application.VolunteerManagement.UpdateSocialMedia
{
    public interface IUpdateSocialMediaHandler
    {
        Task<Result<Guid>> Execute(UpdateSocialMediaCommand request, CancellationToken cancellationToken = default);
    }
}