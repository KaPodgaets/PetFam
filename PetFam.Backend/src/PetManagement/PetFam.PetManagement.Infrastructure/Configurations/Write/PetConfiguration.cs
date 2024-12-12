using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.PetManagement.Domain.Entities;
using PetFam.Shared.Dtos.ValueObjects;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.ValueObjects.Pet;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.PetManagement.Infrastructure.Configurations.Write
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

            builder.Property(p => p.Photos)
                .HasValueObjectsCollectionJsonConversion(
                    photos => new PetPhotoDto(photos.FilePath, photos.IsMain),
                    json => PetPhoto.Create(json.Filepath, json.IsMain).Value)
                .HasColumnType("jsonb")
                .HasColumnName("photos");

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

            builder.OwnsOne(gi => gi.Address, ab =>
            {
                ab.ToJson();

                ab.Property(ai => ai.Country)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("country");

                ab.Property(ai => ai.City)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("city");

                ab.Property(ai => ai.Street)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                    .HasColumnName("street");

                ab.Property(ai => ai.Building)
                    .HasColumnName("building");

                ab.Property(ai => ai.Litteral)
                    .IsRequired()
                    .HasMaxLength(Constants.ONE_CHAR_LIMIT)
                    .HasColumnName("letteral");
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

            builder.OwnsOne(p => p.GeneralInfo, gi =>
            {
                gi.ToJson();

                gi.Property(p => p.Comment).HasColumnName("general_info_comment");
                gi.Property(p => p.Color).HasColumnName("color");
                gi.Property(p => p.PhoneNumber).HasColumnName("phone_number");
                gi.Property(p => p.Height).HasColumnName("height");
                gi.Property(p => p.Weight).HasColumnName("weight");
            });

            builder.ComplexProperty(p => p.HealthInfo, hib =>
            {
                hib.Property(a => a.IsCastrated).HasColumnName("is_castrated");
                hib.Property(a => a.IsVaccinated).HasColumnName("is_vaccinated");
                hib.Property(a => a.BirthDate).HasColumnName("birthdate");
                hib.Property(a => a.Comment).HasColumnName("health_info_comment");
            });

            builder.Property(x => x.IsDeleted)
                .HasColumnName("is_deleted");
            
            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
