namespace PetFam.Volunteers.Application.VolunteerManagement
{
    public interface IVolunteerRepository
    {
        Task<Result<Guid>> Add(Volunteer model, CancellationToken cancellationToken = default);
        Task<Result<Volunteer>> GetById(VolunteerId id, CancellationToken cancellationToken = default);
        Task<Result<Volunteer>> GetByEmail(Email email, CancellationToken cancellationToken = default);
        Task<Result<Guid>> Update(Volunteer volunteer, CancellationToken cancellationToken = default);
        Task<Result<Guid>> Delete(Volunteer volunteer, CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<Volunteer>>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}