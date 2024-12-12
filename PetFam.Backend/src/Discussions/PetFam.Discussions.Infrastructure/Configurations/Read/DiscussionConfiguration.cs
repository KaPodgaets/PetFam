using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.Discussions.Domain;

namespace PetFam.Discussions.Infrastructure.Configurations.Read;

public class DiscussionConfiguration:IEntityTypeConfiguration<Discussion>
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
        builder.OwnsOne(x => x.Messages, user => { user.ToJson(); });
    }
}