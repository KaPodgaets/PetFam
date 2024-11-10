﻿using PetFam.Domain.Shared;

namespace PetFam.Application.VolunteerManagement.Commands.Create
{
    public interface ICreateVolunteerHandler
    {
        Task<Result<Guid>> ExecuteAsync(CreateVolunteerCommand request, CancellationToken cancellationToken);
    }
}