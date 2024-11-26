using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Infrastructure.Configurations.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<IdentityUserRole<Guid>>();

        builder.HasOne(u => u.AdminAccount)
            .WithOne()
            .HasForeignKey<User>(u => u.AdminAccountId);

        builder.HasOne(u => u.VolunteerAccount)
            .WithOne()
            .HasForeignKey<User>(u => u.VolunteerAccountId);

        builder.HasOne(u => u.ParticipantAccount)
            .WithOne()
            .HasForeignKey<User>(u => u.ParticipantAccountId);
    }
}