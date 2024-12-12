using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Discussions.Domain;
using PetFam.Shared.Dtos.ValueObjects;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel.ValueObjects.Pet;

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
        builder.OwnsOne(x => x.Users, user => { user.ToJson(); });

        builder.HasMany(x => x.Messages)
            .WithOne()
            .HasForeignKey(x => x.DiscussionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // builder.Property(p => p.Users)
        //     .HasValueObjectsCollectionJsonConversion(
        //         user => new UserDto(user.UserId, user.Name),
        //         json => User.Create(json.UserId, json.Name))
        //     .HasColumnType("jsonb")
        //     .HasColumnName("users");
        
        // builder.Property(typeof(List<User>), "_users")
        //     .HasConversion(
        //         users => JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = false }),
        //         json => JsonSerializer.Deserialize<List<User>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<User>()
        //     )
        //     .HasColumnType("jsonb") // Use 'jsonb' for PostgreSQL, 'nvarchar(max)' for SQL Server
        //     .HasColumnName("users");
    }
}

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");
        builder.HasKey(x => x.Id)
            .HasName("id");

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => MessageId.Create(value))
            .HasColumnName("id");

        builder.Property(p => p.Text)
            .HasColumnName("text");
        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at");
        builder.Property(p => p.IsEdited)
            .HasColumnName("is_edited");
        builder.Property(m => m.UserId)
            .HasColumnName("user_id");
    }
}