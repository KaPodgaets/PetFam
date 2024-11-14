namespace PetFam.PetManagement.Infrastructure.Configurations.Read;

public class SpeciesDtoConfiguration :IEntityTypeConfiguration<SpeciesDto>
{
    public void Configure(EntityTypeBuilder<SpeciesDto> builder)
    {
        builder.ToTable("species");
    }
}