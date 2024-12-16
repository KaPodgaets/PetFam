using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Discussions.Application.Dtos;
using PetFam.Discussions.Domain;
using PetFam.Shared.Extensions;

namespace PetFam.Discussions.Infrastructure.Configurations.Read;

public class DiscussionConfiguration:IEntityTypeConfiguration<Discussion>
{
    public void Configure(EntityTypeBuilder<Discussion> builder)
    {
        builder.ToTable("discussions");
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => DiscussionId.Create(value));

        builder.Property(p => p.RelationId)
            .HasColumnName("relation_id");
        builder.Property(p => p.IsClosed)
            .HasColumnName("is_closed");
        
        builder.HasMany(x => x.Messages)
            .WithOne()
            .HasForeignKey("discussion_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.Users)
            .HasValueObjectsCollectionJsonConversion(
                user => new UserDto(user.UserId, user.Name),
                json => User.Create(json.UserId, json.Name).Value)
            .HasColumnType("jsonb")
            .HasColumnName("users");
    }
}