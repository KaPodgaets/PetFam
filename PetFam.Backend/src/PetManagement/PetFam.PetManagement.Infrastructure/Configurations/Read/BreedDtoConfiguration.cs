namespace PetFam.PetManagement.Infrastructure.Configurations.Read;

public class BreedDtoConfiguration :IEntityTypeConfiguration<BreedDto>
{
    public void Configure(EntityTypeBuilder<BreedDto> builder)
    {
        builder.ToTable("breed");
    }
}