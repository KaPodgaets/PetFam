using PetFam.Shared.Abstructions;

namespace PetFam.Application.VolunteerManagement.Queries.GetPetById;

public record GetPetByIdQuery(Guid Id):IQuery;