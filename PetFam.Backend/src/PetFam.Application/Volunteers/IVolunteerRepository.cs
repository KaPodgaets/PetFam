using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers
{
    public interface IVolunteerRepository
    {
        Task<Result<Guid>> Add(Volunteer model, CancellationToken cancellationToken = default);
        Task<Result<Volunteer>> GetById(VolunteerId id);
        Task<Result<Volunteer>> GetByEmail(Email email);
    }
}