﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFam.Discussions.Domain;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Discussions.Application.Commands.Close;

public class CloseDiscussionHandler:ICommandHandler<Guid,CloseDiscussionCommand>
{
    private readonly ILogger<ICommandHandler<Guid,CloseDiscussionCommand>> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDiscussionsRepository _discussionsRepository;
    private readonly IValidator<CloseDiscussionCommand> _validator;
    
    public CloseDiscussionHandler(
        ILogger<ICommandHandler<Guid, CloseDiscussionCommand>> logger,
        [FromKeyedServices(Modules.Discussions)] IUnitOfWork unitOfWork, 
        IDiscussionsRepository discussionsRepository, 
        IValidator<CloseDiscussionCommand> validator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _discussionsRepository = discussionsRepository;
        _validator = validator;
    }
    public async Task<Result<Guid>> ExecuteAsync(
        CloseDiscussionCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();

        var getDiscussionResult = await _discussionsRepository
            .GetById(DiscussionId.Create(command.Id), cancellationToken);
        if (getDiscussionResult.IsSuccess)
            return Errors.General.NotFound("Discussion Not Found").ToErrorList();
        
        getDiscussionResult.Value.Close();
        
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
            _logger.LogError("Can't close discussion {id}. Error - {error}", command.Id, e.Message);
            transaction.Rollback();
            return Errors.General.Failure().ToErrorList();
        }

        return result.Value;
    }
}