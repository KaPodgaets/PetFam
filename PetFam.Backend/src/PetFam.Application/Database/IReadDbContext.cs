using Microsoft.EntityFrameworkCore;
using PetFam.Application.Dtos;

namespace PetFam.Application.Database
{
    public interface IReadDbContext
    {
        public IQueryable<VolunteerDto> Volunteers { get; }
        public IQueryable<PetDto> Pets { get; }
    }
}
