using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Infrastructure.Configurations.Write;

public class VolunteerAccountConfiguration:IEntityTypeConfiguration<VolunteerAccount>
{
    public void Configure(EntityTypeBuilder<VolunteerAccount> builder)
    {
        builder.ToTable("volunteer_accounts");
        builder.HasKey(x => x.Id);
    }
}