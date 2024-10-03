using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers
{
    public interface IVolunteerRepository
    {
        Task<Result<Guid>> Add(Volunteer model, CancellationToken cancellationToken = default);
        Task<Result<Volunteer>> GetById(VolunteerId id, CancellationToken cancellationToken = default);
        Task<Result<Volunteer>> GetByEmail(Email email, CancellationToken cancellationToken = default);
        Task<Result<Guid>> Update(Volunteer volunteer, CancellationToken cancellationToken = default);
    }
}