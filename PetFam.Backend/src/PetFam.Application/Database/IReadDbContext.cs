using Microsoft.EntityFrameworkCore;
using PetFam.Domain.SpeciesManagement;
using PetFam.Shared.Dtos;

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
