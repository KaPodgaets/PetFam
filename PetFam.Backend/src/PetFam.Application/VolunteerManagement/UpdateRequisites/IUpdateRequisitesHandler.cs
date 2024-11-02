using PetFam.Domain.Shared;

namespace PetFam.Application.VolunteerManagement.UpdateRequisites
{
    public interface IUpdateRequisitesHandler
    {
        Task<Result<Guid>> Execute(UpdateRequisitesCommand request, CancellationToken cancellationToken = default);
    }
}