using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Domain.Pet;
using PetFam.Domain.Shared;

namespace PetFam.Infrastructure.Configurations
{
    public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("species");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    b => b.Value,
                    v => SpeciesId.Create(v))
                .IsRequired();

            builder.Property(s => s.Name)
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .IsRequired();

            builder.HasMany(s => s.Breeds)
                .WithOne()
                .IsRequired();
        }
    }
}
