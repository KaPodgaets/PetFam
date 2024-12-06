using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Accounts.Application.DataModels;

namespace PetFam.Accounts.Infrastructure.Configurations.Read;

public class UserRoleDataModelConfiguration:IEntityTypeConfiguration<UserRoleDataModel>
{
    public void Configure(EntityTypeBuilder<UserRoleDataModel> builder)
    {
        builder.ToTable("user_roles");
        builder.HasKey(x => new {x.UserId, x.RoleId});
    }
}