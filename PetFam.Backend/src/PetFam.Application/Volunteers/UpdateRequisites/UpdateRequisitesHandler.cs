using Microsoft.Extensions.Logging;
using PetFam.Application.Volunteers.UpdateMainInfo;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers.UpdateRequisites
{
    public class UpdateRequisitesHandler : IUpdateRequisitesHandler
    {
        private readonly IVolunteerRepository _repository;
        private readonly ILogger _logger;

        public UpdateRequisitesHandler(
            IVolunteerRepository repository,
            ILogger<UpdateMainInfoHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<Guid>> Handle(
            UpdateRequisitesRequest request,
            CancellationToken cancellationToken = default)
        {
            List<Requisite> requisites = VolunteerDtoMappers.MapRequisiteModel(request.Dto.Requisites);

            var requisiteDetails = RequisitesDetails.Create(requisites).Value;

            var volunteerId = VolunteerId.Create(request.Id);
            var existingVoluntreeByIdResult = await _repository.GetById(volunteerId, cancellationToken);

            if (existingVoluntreeByIdResult.IsFailure)
            {
                return Result<Guid>.Failure(
                    Errors.General.NotFound(
                        request.Id));
            }

            var volunteer = existingVoluntreeByIdResult.Value;
            volunteer.UpdateRequisite(requisiteDetails);

            var updateResult = await _repository.Update(volunteer, cancellationToken);
            if (updateResult.IsFailure)
            {
                return Result<Guid>.Failure(
                    Errors.General.Failure());
            }

            _logger.LogInformation(
                "Requisites for volunteer with {id} were updated",
                volunteer.Id.Value);

            return updateResult;
        }
    }
}
