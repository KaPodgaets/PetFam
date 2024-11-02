using Microsoft.Extensions.Logging;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.VolunteerManagement.UpdateMainInfo
{
    public class UpdateMainInfoHandler : IUpdateMainInfoHandler
    {
        private readonly IVolunteerRepository _repository;
        private readonly ILogger _logger;

        public UpdateMainInfoHandler(
            IVolunteerRepository repository,
            ILogger<UpdateMainInfoHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<Guid>> Execute(
            UpdateMainInfoCommand request,
            CancellationToken cancellationToken = default)
        {
            var fullName = FullName.Create(
                request.Dto.FullNameDto.FirstName,
                request.Dto.FullNameDto.LastName,
                request.Dto.FullNameDto.Patronimycs)
                .Value;

            var email = Email.Create(request.Dto.Email).Value;

            var generalInformation = GeneralInformation.Create(
                request.Dto.GeneralInformationDto.BioEducation,
                request.Dto.GeneralInformationDto.ShortDescription)
                .Value;

            var volunteerId = VolunteerId.Create(request.Id);
            var existingVoluntreeByIdResult = await _repository.GetById(volunteerId, cancellationToken);

            if (existingVoluntreeByIdResult.IsFailure)
            {
                return Result<Guid>.Failure(
                    Errors.General.NotFound(
                        request.Id));
            }

            var volunteer = existingVoluntreeByIdResult.Value;
            volunteer.UpdateMainInfo(fullName, email, request.Dto.AgeOfExpirience, generalInformation);

            var updateResult = await _repository.Update(volunteer, cancellationToken);
            if (updateResult.IsFailure)
            {
                return Result<Guid>.Failure(
                    Errors.General.Failure());
            }

            _logger.LogInformation(
                "Name for volunteer with {id} was updated",
                volunteer.Id.Value);

            return updateResult;
        }
    }
}
