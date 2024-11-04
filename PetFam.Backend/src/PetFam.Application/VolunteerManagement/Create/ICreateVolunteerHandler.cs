using PetFam.Domain.Shared;

namespace PetFam.Application.VolunteerManagement.Create
{
    public interface ICreateVolunteerHandler
    {
        Task<Result<Guid>> Execute(CreateVolunteerCommand request, CancellationToken cancellationToken);
    }
}