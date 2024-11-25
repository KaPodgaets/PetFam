﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFam.PetManagement.Domain;
using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.PetManagement.Infrastructure.Configurations.Write
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => VolunteerId.Create(value));

            builder.ComplexProperty(v => v.Email, veb =>
            {
                veb.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_EMAIL_LENGTH)
                    .HasColumnName("email");
            });

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(p => p.Requisites, rb =>
            {
                rb.ToJson();

                rb.OwnsMany(g => g.Value, requisiteBuilder =>
                {
                    requisiteBuilder.Property(p => p.Name)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    requisiteBuilder.Property(p => p.AccountNumber)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    requisiteBuilder.Property(p => p.PaymentInstruction)
                        .HasMaxLength(Constants.MAX_LONG_TEXT_LENGTH);
                });
            });

            builder.OwnsOne(v => v.SocialMediaDetails, smb =>
            {
                smb.ToJson();

                smb.OwnsMany(sm => sm.Value, smlBuilder =>
                {
                    smlBuilder.Property(p => p.Name)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    smlBuilder.Property(p => p.Link)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                });
            });

            builder.OwnsOne(p => p.FullName, fnb =>
            {
                fnb.ToJson();

                fnb.Property(p => p.FirstName)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                fnb.Property(p => p.LastName)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                fnb.Property(p => p.Patronymic)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            });

            builder.OwnsOne(p => p.GeneralInformation1, gib =>
            {
                gib.ToJson();

                gib.Property(p => p.BioEducation)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LONG_TEXT_LENGTH);

                gib.Property(p => p.ShortDescription)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LONG_TEXT_LENGTH);
            });

            builder.Property(x => x.IsDeleted)
                .HasColumnName("is_deleted");
        }
    }
}
