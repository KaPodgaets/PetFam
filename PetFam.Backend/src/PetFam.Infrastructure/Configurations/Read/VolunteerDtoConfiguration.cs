using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Application.Dtos;

namespace PetFam.Infrastructure.Configurations.Read
{
    public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
    {
        public void Configure(EntityTypeBuilder<VolunteerDto> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(p => p.Id);

            //builder.HasMany(v => v.Pets)
            //    .WithOne()
            //    .HasForeignKey(p => p.VolunteerId);
        }
    }
}
