using PetFam.Domain.Shared;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateMainInfo
{
    public interface IUpdateMainInfoHandler
    {
        Task<Result<Guid>> ExecuteAsync(UpdateMainInfoCommand request, CancellationToken cancellationToken = default);
    }
}