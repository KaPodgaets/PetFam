using Microsoft.EntityFrameworkCore;
using PetFam.Shared.Dtos;

namespace PetFam.BreedManagement.Application.Database;

public interface IReadDbContext
{
    public DbSet<SpeciesDto> Species { get; }
    public DbSet<BreedDto> Breeds { get; }
}