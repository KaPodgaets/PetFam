using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Discussions.Domain;
using PetFam.Shared.Extensions;

namespace PetFam.Discussions.Infrastructure.Configurations.Write;

public class DiscussionConfiguration : IEntityTypeConfiguration<Discussion>
{
    public void Configure(EntityTypeBuilder<Discussion> builder)
    {
        builder.ToTable("discussions");
        builder.HasKey(x => x.Id)
            .HasName("id");

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => DiscussionId.Create(value))
            .HasColumnName("id");

        builder.Property(p => p.RelationId)
            .HasColumnName("relation_id");
        builder.Property(p => p.IsClosed)
            .HasColumnName("is_closed");

        builder.HasMany(x => x.Messages)
            .WithOne()
            .HasForeignKey(x => x.DiscussionId)
            .OnDelete(DeleteBehavior.Cascade);

        // builder.OwnsOne(x => x.Users, xb =>
        // {
        //     xb.Property(x => x.FirstUser.UserId).HasColumnName("first_user_id");
        //     xb.Property(x => x.FirstUser.Name).HasColumnName("first_user_name");
        //     xb.Property(x => x.SecondUser.UserId).HasColumnName("second_user_id");
        //     xb.Property(x => x.SecondUser.Name).HasColumnName("second_user_name");
        // });
        
        builder.Property(p => p.Users)
            .HasValueObjectsCollectionJsonConversion(
                user => new UserDto(user.UserId, user.Name),
                json => User.Create(json.UserId, json.Name))
            .HasColumnType("jsonb")
            .HasColumnName("users");
    }
}