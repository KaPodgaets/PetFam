using Microsoft.EntityFrameworkCore;
using PetFam.Application.Dtos;
using PetFam.Domain.SpeciesManagement;

namespace PetFam.Application.Database
{
    public interface IReadDbContext
    {
        public DbSet<VolunteerDto> Volunteers { get; }
        public DbSet<PetDto> Pets { get; }
        public DbSet<SpeciesDto> Species { get; }
        public DbSet<BreedDto> Breeds { get; }
    }
}
