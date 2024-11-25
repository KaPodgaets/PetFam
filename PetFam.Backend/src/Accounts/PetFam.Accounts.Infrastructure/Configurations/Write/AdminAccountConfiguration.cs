using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Infrastructure.Configurations.Write;

public class AdminAccountConfiguration:IEntityTypeConfiguration<AdminAccount>
{
    public void Configure(EntityTypeBuilder<AdminAccount> builder)
    {
        builder.ToTable("admin_accounts");
        builder.HasKey(a => a.Id);
    }
}