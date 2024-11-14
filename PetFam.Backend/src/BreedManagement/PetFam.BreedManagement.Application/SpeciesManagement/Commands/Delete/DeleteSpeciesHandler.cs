using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.BreedManagement.Application.Database;
using PetFam.PetManagement.Contracts;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.BreedManagement.Application.SpeciesManagement.Commands.Delete
{
    public class DeleteSpeciesHandler:ICommandHandler<Guid, DeleteSpeciesCommand>
    {
        private readonly ISpeciesRepository _repository;
        private readonly IVolunteerContract _volunteerContract;
        private readonly IValidator<DeleteSpeciesCommand> _validator;
        private readonly ILogger _logger;

        public DeleteSpeciesHandler(
            ISpeciesRepository repository,
            ILogger<DeleteSpeciesHandler> logger,
            IValidator<DeleteSpeciesCommand> validator,
            IVolunteerContract volunteerContract)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _volunteerContract = volunteerContract;
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
            
            var species = existSpeciesResult.Value;
            
            // check pets of this species exist wiht ReadDBContext

            var isPetsWithDeletingSpeciesExist = await _volunteerContract
                .IsPetsWithSpeciesExisting(species.Id, cancellationToken);
                
            if(isPetsWithDeletingSpeciesExist.Value)
                return Errors.Species.CannotDeleteDueToRelatedRecords(command.Id).ToErrorList();
            
            species.Delete(); // breeds deleted also - cascade

            var deleteResult = await _repository.Update(species, cancellationToken);

            if (deleteResult.IsFailure)
                return Result<Guid>.Failure(deleteResult.Errors);

            _logger.LogInformation(
                "Delete species with {name} with id {id} with all breeds",
                existSpeciesResult.Value.Name,
                existSpeciesResult.Value.Id.Value);

            return deleteResult;
        }
    }
}
