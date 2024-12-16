using PetFam.Shared.Abstractions;

namespace PetFam.Discussions.Application.Queries.GetById;

public record GetByIdQuery(Guid Id):IQuery;