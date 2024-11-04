using PetFam.Domain.Shared;

namespace PetFam.Application.VolunteerManagement.UpdateMainInfo
{
    public interface IUpdateMainInfoHandler
    {
        Task<Result<Guid>> Execute(UpdateMainInfoCommand request, CancellationToken cancellationToken = default);
    }
}