using PetFam.Shared.Abstractions;

namespace PetFam.PetManagement.Application.VolunteerManagement.Queries.GetPetById;

public record GetPetByIdQuery(Guid Id):IQuery;