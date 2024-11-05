using PetFam.Domain.Shared;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateRequisites
{
    public interface IUpdateRequisitesHandler
    {
        Task<Result<Guid>> ExecuteAsync(UpdateRequisitesCommand request, CancellationToken cancellationToken = default);
    }
}