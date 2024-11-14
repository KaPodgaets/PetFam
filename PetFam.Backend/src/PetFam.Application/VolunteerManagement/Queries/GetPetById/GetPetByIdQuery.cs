using PetFam.Shared.Abstractions;

namespace PetFam.Application.VolunteerManagement.Queries.GetPetById;

public record GetPetByIdQuery(Guid Id):IQuery;