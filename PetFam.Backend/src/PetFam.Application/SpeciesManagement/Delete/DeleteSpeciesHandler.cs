using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Application.VolunteerManagement;
using PetFam.Application.Extensions;
using PetFam.Domain.Shared;
using PetFam.Domain.SpeciesManagement;
using PetFam.Application.Interfaces;

namespace PetFam.Application.SpeciesManagement.Delete
{
    public class DeleteSpeciesHandler:ICommandHandler<Guid, DeleteSpeciesCommand>
    {
        private readonly ISpeciesRepository _repository;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<DeleteSpeciesCommand> _validator;
        private readonly ILogger _logger;

        public DeleteSpeciesHandler(
            ISpeciesRepository repository,
            IVolunteerRepository volunteerRepository,
            ILogger<DeleteSpeciesHandler> logger,
            IValidator<DeleteSpeciesCommand> validator)
        {
            _repository = repository;
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _validator = validator;
        }
        public async Task<Result<Guid>> ExecuteAsync(DeleteSpeciesCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            var existSpeciesResult = await _repository.GetById(SpeciesId.Create(command.Id), cancellationToken);

            if (existSpeciesResult.IsFailure)
                return Result<Guid>.Failure(existSpeciesResult.Errors);

            var getVolunteersResult = await _volunteerRepository.GetAllAsync(cancellationToken);
            if (getVolunteersResult.IsFailure)
            {
                return Errors.General.Failure().ToErrorList();
            }
            var allExistingSpecies = getVolunteersResult.Value.SelectMany(x => x.Pets.Select(p => p.SpeciesAndBreed.SpeciesId.Value));

            if(allExistingSpecies.Any(p => p == command.Id))
            {
                return Errors.General.DeletionEntityWithRelation().ToErrorList();
            }

            var deleteResult = await _repository.Delete(existSpeciesResult.Value, cancellationToken);

            if (deleteResult.IsFailure)
                return Result<Guid>.Failure(deleteResult.Errors);

            _logger.LogInformation(
                "Delete species with {name} with id {id}",
                existSpeciesResult.Value.Name,
                existSpeciesResult.Value.Id.Value);

            return deleteResult;
        }
    }
}
