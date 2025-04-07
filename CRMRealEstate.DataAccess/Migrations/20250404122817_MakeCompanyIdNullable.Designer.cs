﻿// <auto-generated />
using System;
using CRMRealEstate.DataAccess.Scripts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRMRealEstate.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250404122817_MakeCompanyIdNullable")]
    partial class MakeCompanyIdNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.Adress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AppartamentNumber")
                        .HasColumnType("integer");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PropertyId")
                        .HasColumnType("integer");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StreetNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.Announcement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CompanyId"));

                    b.Property<DateTime>("CompanyCreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CompanyIdentityNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyPhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CompanyId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AnnouncementId")
                        .HasColumnType("integer");

                    b.Property<double>("Area")
                        .HasColumnType("double precision");

                    b.Property<int>("BathroomsNumber")
                        .HasColumnType("integer");

                    b.Property<int>("ConstructionYear")
                        .HasColumnType("integer");

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("PropertyType")
                        .HasColumnType("integer");

                    b.Property<int>("RoomsNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Utilities")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isAvailable")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AnnouncementId")
                        .IsUnique();

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.UserAnnouncement", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("AnnouncementId")
                        .HasColumnType("integer");

                    b.Property<int?>("AnnouncementId1")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "AnnouncementId");

                    b.HasIndex("AnnouncementId");

                    b.HasIndex("AnnouncementId1");

                    b.ToTable("UsersAnnouncements");
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CompanyId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Roles")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UserCreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.Adress", b =>
                {
                    b.HasOne("CRMRealEstate.DataAccess.Entities.Property", "Property")
                        .WithOne("Adress")
                        .HasForeignKey("CRMRealEstate.DataAccess.Entities.Adress", "PropertyId");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.Announcement", b =>
                {
                    b.HasOne("CRMRealEstate.DataAccess.Entities.Users", "User")
                        .WithMany("Announcements")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.Property", b =>
                {
                    b.HasOne("CRMRealEstate.DataAccess.Entities.Announcement", "Announcement")
                        .WithOne("Property")
                        .HasForeignKey("CRMRealEstate.DataAccess.Entities.Property", "AnnouncementId");

                    b.Navigation("Announcement");
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.UserAnnouncement", b =>
                {
                    b.HasOne("CRMRealEstate.DataAccess.Entities.Announcement", "Announcement")
                        .WithMany("Users")
                        .HasForeignKey("AnnouncementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRMRealEstate.DataAccess.Entities.Announcement", null)
                        .WithMany("UserAnnouncements")
                        .HasForeignKey("AnnouncementId1");

                    b.HasOne("CRMRealEstate.DataAccess.Entities.Users", "User")
                        .WithMany("FavoritesAnnouncements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Announcement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.Users", b =>
                {
                    b.HasOne("CRMRealEstate.DataAccess.Entities.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Company");
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.Announcement", b =>
                {
                    b.Navigation("Property")
                        .IsRequired();

                    b.Navigation("UserAnnouncements");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.Company", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.Property", b =>
                {
                    b.Navigation("Adress")
                        .IsRequired();
                });

            modelBuilder.Entity("CRMRealEstate.DataAccess.Entities.Users", b =>
                {
                    b.Navigation("Announcements");

                    b.Navigation("FavoritesAnnouncements");
                });
#pragma warning restore 612, 618
        }
    }
}
