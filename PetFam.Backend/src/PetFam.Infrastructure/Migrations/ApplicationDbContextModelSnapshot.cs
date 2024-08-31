﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetFam.Infrastructure;

#nullable disable

namespace PetFam.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetFam.Domain.Pet.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_date");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("breed");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("color");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_date");

                    b.Property<string>("GeneralInfo")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("general_info");

                    b.Property<string>("HealthInfo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("health_info");

                    b.Property<double>("Height")
                        .HasColumnType("double precision")
                        .HasColumnName("height");

                    b.Property<bool>("IsCastrated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_castrated");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccinated");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("nick_name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("species");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.Property<Guid?>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("PetFam.Domain.Volunteer.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("AgesOfExpirience")
                        .HasColumnType("integer")
                        .HasColumnName("ages_of_expirience");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("GeneralInformation")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("general_information");

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("PetFam.Domain.Pet.Pet", b =>
                {
                    b.HasOne("PetFam.Domain.Volunteer.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.OwnsOne("PetFam.Domain.Pet.AccountInfo", "AccountInfo", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("BankName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("AccountInfo");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");
                        });

                    b.OwnsOne("PetFam.Domain.Pet.Gallery", "Gallery", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("Gallery");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");

                            b1.OwnsMany("PetFam.Domain.Pet.PetPhoto", "Value", b2 =>
                                {
                                    b2.Property<Guid>("GalleryPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("FilePath")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<bool>("IsMain")
                                        .HasColumnType("boolean");

                                    b2.HasKey("GalleryPetId", "Id")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("GalleryPetId")
                                        .HasConstraintName("fk_pets_pets_gallery_pet_id");
                                });

                            b1.Navigation("Value");
                        });

                    b.Navigation("AccountInfo");

                    b.Navigation("Gallery");
                });

            modelBuilder.Entity("PetFam.Domain.Volunteer.Volunteer", b =>
                {
                    b.OwnsOne("PetFam.Domain.Volunteer.FullName", "FullName", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("full_name_first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("full_name_last_name");

                            b1.Property<string>("Patronymic")
                                .HasColumnType("text")
                                .HasColumnName("full_name_patronymic");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("FullName");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");
                        });

                    b.OwnsOne("PetFam.Domain.Volunteer.RequisitesDetails", "Requisites", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("Requisites");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("PetFam.Domain.Volunteer.Requisite", "Value", b2 =>
                                {
                                    b2.Property<Guid>("RequisitesDetailsVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("AccountNumber")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("PaymentInstruction")
                                        .IsRequired()
                                        .HasMaxLength(1000)
                                        .HasColumnType("character varying(1000)");

                                    b2.HasKey("RequisitesDetailsVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisitesDetailsVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_requisites_details_volunteer_id");
                                });

                            b1.Navigation("Value");
                        });

                    b.OwnsOne("PetFam.Domain.Volunteer.SocialMediaDetails", "SocialMediaDetails", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("SocialMediaDetails");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("PetFam.Domain.Volunteer.SocialMediaLink", "Value", b2 =>
                                {
                                    b2.Property<Guid>("SocialMediaDetailsVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Link")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.HasKey("SocialMediaDetailsVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialMediaDetailsVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_social_media_details_volunteer_id");
                                });

                            b1.Navigation("Value");
                        });

                    b.Navigation("FullName")
                        .IsRequired();

                    b.Navigation("Requisites");

                    b.Navigation("SocialMediaDetails");
                });

            modelBuilder.Entity("PetFam.Domain.Volunteer.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
