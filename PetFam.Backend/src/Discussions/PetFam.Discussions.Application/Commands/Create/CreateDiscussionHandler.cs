using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFam.Discussions.Domain;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Discussions.Application.Commands.Create;

public class CreateDiscussionHandler:ICommandHandler<Guid,CreateDiscussionCommand>
{
    private readonly ILogger<ICommandHandler<Guid,CreateDiscussionCommand>> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDiscussionsRepository _discussionsRepository;
    private readonly IValidator<CreateDiscussionCommand> _validator;
    public CreateDiscussionHandler(
        ILogger<ICommandHandler<Guid, CreateDiscussionCommand>> logger,
        [FromKeyedServices(Modules.Discussions)] IUnitOfWork unitOfWork, 
        IDiscussionsRepository discussionsRepository, 
        IValidator<CreateDiscussionCommand> validator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _discussionsRepository = discussionsRepository;
        _validator = validator;
    }
    public async Task<Result<Guid>> ExecuteAsync(
        CreateDiscussionCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();

        // TODO: check that users exist
        // TODO: how to check that relatedObject exists?

        List<User> users = [];
        
        foreach (var userDto in command.Users)
        {
            var user = User.Create(userDto.UserId, userDto.Name).Value;
            users.Add(user);
        } 
        
        var discussion = Discussion.Create(
            command.RelationId,
            users).Value;

        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        
        var result = await _discussionsRepository.Add(discussion, cancellationToken);
        if (result.IsFailure)
            return result;
        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            transaction.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError("Can't create new discussion. Error - {error}", e.Message);
            transaction.Rollback();
            return Errors.General.Failure().ToErrorList();
        }

        return result.Value;
    }
}