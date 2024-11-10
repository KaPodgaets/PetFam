using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Application.Dtos;
using PetFam.Application.Dtos.ValueObjects;
using System.Text.Json;

namespace PetFam.Infrastructure.Configurations.Read
{
    public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
    {
        public void Configure(EntityTypeBuilder<PetDto> builder)
        {
            builder.ToTable("pets");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Photos)
                .HasConversion(
                    photos => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),

                    json => JsonSerializer.Deserialize<PetPhotoDto[]>(json, JsonSerializerOptions.Default)!);

            builder.OwnsOne(p => p.SpeciesAndBreed, sbb =>
            {
                sbb.ToJson();

                sbb.Property(ai => ai.SpeciesId)
                    .IsRequired();

                sbb.Property(ai => ai.BreedId)
                    .IsRequired();
            });
        }
    }
}
