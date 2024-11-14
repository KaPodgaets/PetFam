using System.Text.Json;

namespace PetFam.PetManagement.Infrastructure.Configurations.Read
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
            
            builder.ComplexProperty(p => p.HealthInfo, hib =>
            {
                hib.Property(a => a.IsCastrated).HasColumnName("is_castrated");
                hib.Property(a => a.IsVaccinated).HasColumnName("is_vaccinated");
                hib.Property(a => a.BirthDate).HasColumnName("birthdate");
                hib.Property(a => a.Comment).HasColumnName("health_info_comment");
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
            
            builder.OwnsOne(gi => gi.Address, ab =>
            {
                ab.ToJson();

                ab.Property(ai => ai.Country)
                    .HasColumnName("country");

                ab.Property(ai => ai.City)
                    .HasColumnName("city");

                ab.Property(ai => ai.Street)
                    .HasColumnName("street");

                ab.Property(ai => ai.Building)
                    .HasColumnName("building");

                ab.Property(ai => ai.Litteral)
                    .HasColumnName("letteral");
            });
        }
    }
}
