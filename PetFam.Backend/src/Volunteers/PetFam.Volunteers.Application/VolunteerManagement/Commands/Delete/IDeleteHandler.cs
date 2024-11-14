namespace PetFam.Volunteers.Application.VolunteerManagement.Commands.Delete
{
    public interface IDeleteHandler
    {
        Task<Result<Guid>> ExecuteAsync(DeleteCommand request, CancellationToken cancellationToken = default);
    }
}