using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Shared.Dtos;

namespace PetFam.BreedManagement.Infrastructure.Configurations.Read;

public class SpeciesDtoConfiguration :IEntityTypeConfiguration<SpeciesDto>
{
    public void Configure(EntityTypeBuilder<SpeciesDto> builder)
    {
        builder.ToTable("species");
    }
}