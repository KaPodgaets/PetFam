using PetFam.Shared.Abstractions;

namespace PetFam.BreedManagement.Application.SpeciesManagement.Queries.GetBreedById;

public record GetBreedByIdQuery(Guid BreedId) : IQuery;