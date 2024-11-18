using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.BreedManagement.Application.Database;
using PetFam.BreedManagement.Domain.Entities;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.BreedManagement.Application.SpeciesManagement.Commands.CreateBreed
{
    public class CreateBreedHandler:ICommandHandler<Guid, CreateBreedCommand>
    {
        private readonly ISpeciesRepository _repository;
        private readonly IValidator<CreateBreedCommand> _validator;
        private readonly ILogger _logger;

        public CreateBreedHandler(
            ISpeciesRepository repository,
            ILogger<CreateBreedHandler> logger,
            IValidator<CreateBreedCommand> validator)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
        }
        public async Task<Result<Guid>> ExecuteAsync(CreateBreedCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            var existSpeciesResult = await _repository.GetById(SpeciesId.Create(command.SpeciesId), cancellationToken);

            if (existSpeciesResult.IsFailure)
                return Result<Guid>.Failure(existSpeciesResult.Errors);

            var species = existSpeciesResult.Value;

            var createBreedResult = Breed.Create(BreedId.NewId(), command.Name);

            if (createBreedResult.IsFailure)
                return Result<Guid>.Failure(createBreedResult.Errors);

            var addBreedResult = species.AddBreed(createBreedResult.Value);

            if (addBreedResult.IsFailure)
                return Result<Guid>.Failure(addBreedResult.Errors);

            await _repository.Update(species, cancellationToken);

            _logger.LogInformation(
                "Add breed with {breedName} and {breedId} to species with id {speciesId}",
                command.Name,
                createBreedResult.Value.Id.Value,
                command.SpeciesId);

            return addBreedResult;
        }
    }
}
