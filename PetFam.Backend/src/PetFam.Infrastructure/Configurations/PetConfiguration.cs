using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Domain.Pet;
using PetFam.Domain.Shared;

namespace PetFam.Infrastructure.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pets");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => PetId.Create(value))
                .IsRequired();

            builder.Property(p => p.NickName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(p => p.GeneralInfo)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LONG_TEXT_LENGTH);

            builder.OwnsOne(p => p.Gallery, gb =>
            {
                gb.ToJson();

                gb.OwnsMany(g => g.Value, photoBuilder =>
                {
                    photoBuilder.Property(p => p.FilePath)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    photoBuilder.Property(p => p.IsMain)
                        .IsRequired();
                });
            });

            builder.OwnsOne(p => p.AccountInfo, aib =>
            {
                aib.ToJson();

                aib.Property(ai => ai.BankName)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                aib.Property(ai => ai.Number)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            });

            builder.OwnsOne(p => p.SpeciesAndBreed, sbb =>
            {
                sbb.ToJson();

                sbb.Property(ai => ai.SpeciesId)
                    .HasConversion(
                    id => id.Value,
                    value => SpeciesId.Create(value))
                    .IsRequired();

                sbb.Property(ai => ai.BreedId)
                    .IsRequired();
            });

            builder.ComplexProperty(p => p.Address, ab =>
            {
                ab.Property(ai => ai.Country)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                ab.Property(ai => ai.City)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                ab.Property(ai => ai.Street)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                ab.Property(ai => ai.Building);

                ab.Property(ai => ai.Litteral)
                    .IsRequired()
                    .HasMaxLength(Constants.ONE_CHAR_LIMIT);
            });

        }
    }
}
