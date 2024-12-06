using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Accounts.Application.DataModels;

namespace PetFam.Accounts.Infrastructure.Configurations.Read;

public class UserDataModelConfiguration:IEntityTypeConfiguration<UserDataModel>
{
    public void Configure(EntityTypeBuilder<UserDataModel> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(x => x.Id);
    }
}