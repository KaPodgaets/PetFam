using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.BreedManagement.Domain;
using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.BreedManagement.Infrastructure.Configurations.Write
{
    public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("species");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => SpeciesId.Create(value));

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.HasMany(s => s.Breeds)
                .WithOne()
                .HasForeignKey("species_id");

            builder.Property(x => x.IsDeleted)
                .HasColumnName("is_deleted");
            
            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
