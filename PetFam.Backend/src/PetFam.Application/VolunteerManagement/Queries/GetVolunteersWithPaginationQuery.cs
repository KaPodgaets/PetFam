using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Queries
{
    public record GetVolunteersWithPaginationQuery(int PageNumber, int PageSize):IQuery;
}
