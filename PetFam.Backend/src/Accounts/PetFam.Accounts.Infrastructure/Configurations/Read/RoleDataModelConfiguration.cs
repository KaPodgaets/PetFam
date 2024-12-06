using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Accounts.Application.DataModels;

namespace PetFam.Accounts.Infrastructure.Configurations.Read;

public class RoleDataModelConfiguration : IEntityTypeConfiguration<RoleDataModel>
{
    public void Configure(EntityTypeBuilder<RoleDataModel> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(x => x.Id);
    }
}