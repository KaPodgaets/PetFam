using PetFam.Domain.Shared;

namespace PetFam.Application.VolunteerManagement.Delete
{
    public interface IDeleteHandler
    {
        Task<Result<Guid>> Execute(DeleteCommand request, CancellationToken cancellationToken = default);
    }
}