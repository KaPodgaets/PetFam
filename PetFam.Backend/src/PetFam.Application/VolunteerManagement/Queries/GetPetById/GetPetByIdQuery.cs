using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Queries.GetPetById;

public record GetPetByIdQuery(Guid Id):IQuery;