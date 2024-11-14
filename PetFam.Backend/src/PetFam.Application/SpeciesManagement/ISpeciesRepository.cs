﻿using PetFam.Domain.SpeciesManagement;
using PetFam.Domain.Volunteer;
using PetFam.Shared.Shared;
using PetFam.Shared.ValueObjects.Species;

namespace PetFam.Application.SpeciesManagement
{
    public interface ISpeciesRepository
    {
        Task<Result<Guid>> Add(Species model, CancellationToken cancellationToken = default);
        Task<Result<Guid>> Delete(Species species, CancellationToken cancellationToken = default);
        Task<Result<Species>> GetById(SpeciesId id, CancellationToken cancellationToken = default);
        Task<Result<Species>> GetByName(string name, CancellationToken cancellationToken = default);
        Task<Result<Guid>> Update(Species species, CancellationToken cancellationToken = default);
    }
}