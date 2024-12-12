using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.Infrastructure.Configurations.Write;

public class ApplicationConfiguration : IEntityTypeConfiguration<VolunteeringApplication>
{
    public void Configure(EntityTypeBuilder<VolunteeringApplication> builder)
    {
        builder.ToTable("volunteering_applications");
        builder.HasKey(x => x.Id)
            .HasName("id");

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteeringApplicationId.Create(value))
            .HasColumnName("id");

        builder.Property(p => p.AdminId)
            .IsRequired(false)
            .HasColumnName("admin_id");
        builder.Property(p => p.UserId)
            .HasColumnName("user_id");
        builder.Property(p => p.VolunteerInfo)
            .HasColumnName("volunteer_info");
        builder.Property(p => p.Status)
            .HasColumnName("status");
        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at");
        builder.Property(p => p.RejectionComment)
            .HasColumnName("rejection_comment");
        builder.Property(p => p.DiscussionId)
            .HasColumnName("discussion_id");
    }
}