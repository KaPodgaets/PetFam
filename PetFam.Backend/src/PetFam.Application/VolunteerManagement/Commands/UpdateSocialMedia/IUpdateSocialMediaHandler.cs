using PetFam.Domain.Shared;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateSocialMedia
{
    public interface IUpdateSocialMediaHandler
    {
        Task<Result<Guid>> ExecuteAsync(UpdateSocialMediaCommand request, CancellationToken cancellationToken = default);
    }
}