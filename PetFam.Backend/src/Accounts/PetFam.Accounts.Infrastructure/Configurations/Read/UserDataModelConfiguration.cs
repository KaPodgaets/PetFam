using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Accounts.Application.DataModels;

namespace PetFam.Accounts.Infrastructure.Configurations.Read;

public class UserDataModelConfiguration:IEntityTypeConfiguration<UserDataModel>
{
    public void Configure(EntityTypeBuilder<UserDataModel> builder)
    {
        builder.ToTable("users");
        
        builder.HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<UserRoleDataModel>(
                e => e.HasOne<UserDataModel>()
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey("UserId")
            );
    }
}