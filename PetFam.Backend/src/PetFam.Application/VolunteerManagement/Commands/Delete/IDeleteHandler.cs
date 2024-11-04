using PetFam.Domain.Shared;

namespace PetFam.Application.VolunteerManagement.Commands.Delete
{
    public interface IDeleteHandler
    {
        Task<Result<Guid>> Execute(DeleteCommand request, CancellationToken cancellationToken = default);
    }
}