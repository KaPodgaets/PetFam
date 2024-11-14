namespace PetFam.Species.Application.Database;

public interface IReadDbContextSpecies
{
    public DbSet<VolunteerDto> Volunteers { get; }
    public DbSet<PetDto> Pets { get; }
    public DbSet<SpeciesDto> Species { get; }
    public DbSet<BreedDto> Breeds { get; }
}