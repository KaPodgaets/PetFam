using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Application.Dtos;

namespace PetFam.Infrastructure.Configurations.Read;

public class BreedDtoConfiguration :IEntityTypeConfiguration<BreedDto>
{
    public void Configure(EntityTypeBuilder<BreedDto> builder)
    {
        builder.ToTable("breed");
    }
}