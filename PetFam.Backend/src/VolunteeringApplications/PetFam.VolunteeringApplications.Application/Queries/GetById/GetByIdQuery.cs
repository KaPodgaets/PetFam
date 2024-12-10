using PetFam.Shared.Abstractions;

namespace PetFam.VolunteeringApplications.Application.Queries.GetById;

public record GetByIdQuery(Guid Id):IQuery;