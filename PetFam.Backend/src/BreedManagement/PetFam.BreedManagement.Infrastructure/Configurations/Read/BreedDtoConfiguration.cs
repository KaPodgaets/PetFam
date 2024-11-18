using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Shared.Dtos;

namespace PetFam.BreedManagement.Infrastructure.Configurations.Read;

public class BreedDtoConfiguration :IEntityTypeConfiguration<BreedDto>
{
    public void Configure(EntityTypeBuilder<BreedDto> builder)
    {
        builder.ToTable("breed");
    }
}