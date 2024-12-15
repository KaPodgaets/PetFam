using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFam.Discussions.Domain;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Discussions.Application.Commands.DeleteMessage;

public class DeleteMessageHandler:ICommandHandler<Guid,DeleteMessageCommand>
{
    private readonly ILogger<ICommandHandler<Guid,DeleteMessageCommand>> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDiscussionsRepository _discussionsRepository;
    private readonly IValidator<DeleteMessageCommand> _validator;
    
    public DeleteMessageHandler(
        ILogger<ICommandHandler<Guid, DeleteMessageCommand>> logger,
        [FromKeyedServices(Modules.Discussions)] IUnitOfWork unitOfWork, 
        IDiscussionsRepository discussionsRepository, 
        IValidator<DeleteMessageCommand> validator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _discussionsRepository = discussionsRepository;
        _validator = validator;
    }
    public async Task<Result<Guid>> ExecuteAsync(
        DeleteMessageCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();

        var getDiscussionResult = await _discussionsRepository
            .GetById(DiscussionId.Create(command.DiscussionId), cancellationToken);
        if (getDiscussionResult.IsFailure)
            return Errors.General.NotFound("Discussion Not Found").ToErrorList();
        
        var deleteMessageResult = getDiscussionResult.Value.DeleteMessage(command.MessageId, command.UserId);
        if (deleteMessageResult.IsFailure)
            return deleteMessageResult.Errors;
        
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        
        var result = _discussionsRepository.Update(getDiscussionResult.Value, cancellationToken);
        if (result.IsFailure)
            return result;
        
        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            transaction.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError("Can't delete message {messageId} from discussion {id}. Error - {error}",
                command.MessageId,
                command.DiscussionId,
                e.Message);
            transaction.Rollback();
            return Errors.General.Failure().ToErrorList();
        }

        return result.Value;
    }
}