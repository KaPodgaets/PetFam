using Microsoft.EntityFrameworkCore;
using PetFam.Application.Dtos;

namespace PetFam.Application.Database
{
    public interface IReadDbContext
    {
        public DbSet<VolunteerDto> Volunteers { get; }
        public DbSet<PetDto> Pets { get; }
    }
}
