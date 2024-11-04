﻿using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Application.Extensions;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.VolunteerManagement.UpdateMainInfo
{
    public class UpdateMainInfoHandler : IUpdateMainInfoHandler
    {
        private readonly IVolunteerRepository _repository;
        private readonly IValidator<UpdateMainInfoCommand> _validator;
        private readonly ILogger _logger;

        public UpdateMainInfoHandler(
            IVolunteerRepository repository,
            ILogger<UpdateMainInfoHandler> logger,
            IValidator<UpdateMainInfoCommand> validator)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> Execute(
            UpdateMainInfoCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            var fullName = FullName.Create(
                command.FullNameDto.FirstName,
                command.FullNameDto.LastName,
                command.FullNameDto.Patronimycs)
                .Value;

            var email = Email.Create(command.Email).Value;

            var generalInformation = GeneralInformation.Create(
                command.GeneralInformationDto.BioEducation,
                command.GeneralInformationDto.ShortDescription)
                .Value;

            var volunteerId = VolunteerId.Create(command.Id);
            var existingVolunteerByIdResult = await _repository.GetById(volunteerId, cancellationToken);

            if (existingVolunteerByIdResult.IsFailure)
                return existingVolunteerByIdResult.Errors;

            var volunteer = existingVolunteerByIdResult.Value;
            volunteer.UpdateMainInfo(fullName, email, command.AgeOfExpirience, generalInformation);

            var updateResult = await _repository.Update(volunteer, cancellationToken);
            if (updateResult.IsFailure)
                return updateResult.Errors;

            _logger.LogInformation(
                "Name for volunteer with {id} was updated",
                volunteer.Id.Value);

            return updateResult;
        }
    }
}
