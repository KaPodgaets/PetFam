using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.VolunteerManagement.Querries
{
    public class GetVolunteersWithPaginationHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        public GetVolunteersWithPaginationHandler(IVolunteerRepository volunteerRepository)
        {
            _volunteerRepository = volunteerRepository;
        }

        public async Task<IReadOnlyList<Volunteer>> Handle(CancellationToken cancellationToken = default)
        {
            var volunteers = await _volunteerRepository.GetAllAsync(cancellationToken);
            return volunteers.Value;
        }
    }
}
