﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialMedia_LifeCycle.DataAccessEF;

namespace SocialMedia_LifeCycle.Migrations
{
    [DbContext(typeof(LifeCycleContext))]
    [Migration("20210213013026_added_refresh_token")]
    partial class added_refresh_token
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("InteractionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("InteractionId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.Interaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<bool>("FromComment")
                        .HasColumnType("bit");

                    b.Property<byte>("LikeState")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)0);

                    b.Property<Guid>("ParentCommentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PublicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("PublicationId");

                    b.HasIndex("UserId");

                    b.ToTable("Interactions");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.Publication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("LastModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.RefreshTokenInfo", b =>
                {
                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("datetime2");

                    b.HasKey("RefreshToken", "UserName");

                    b.ToTable("RefreshTokenInfos");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.RelatedTag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<bool>("FromUser")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PublicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.HasIndex("TagId");

                    b.HasIndex("UserId");

                    b.ToTable("RelatedTag");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.Relation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid>("AskedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("RelationNature")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("Since")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<bool>("Stablished")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("StarterUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AskedUserId");

                    b.HasIndex("StarterUserId");

                    b.ToTable("Relations");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<Guid?>("BackgroundPictureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Explicit")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfilePictureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RegisterDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.Comment", b =>
                {
                    b.HasOne("SocialMedia_LifeCycle.Domain.Models.Interaction", "Interaction")
                        .WithMany("Comments")
                        .HasForeignKey("InteractionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMedia_LifeCycle.Domain.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Interaction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.Interaction", b =>
                {
                    b.HasOne("SocialMedia_LifeCycle.Domain.Models.Comment", "ParentComment")
                        .WithMany("Interactions")
                        .HasForeignKey("ParentCommentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SocialMedia_LifeCycle.Domain.Models.Publication", "Publication")
                        .WithMany("Interactions")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMedia_LifeCycle.Domain.Models.User", "User")
                        .WithMany("Interactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ParentComment");

                    b.Navigation("Publication");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.Publication", b =>
                {
                    b.HasOne("SocialMedia_LifeCycle.Domain.Models.User", "User")
                        .WithMany("Publications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.RelatedTag", b =>
                {
                    b.HasOne("SocialMedia_LifeCycle.Domain.Models.Publication", "Publication")
                        .WithMany("RelatedTags")
                        .HasForeignKey("PublicationId");

                    b.HasOne("SocialMedia_LifeCycle.Domain.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMedia_LifeCycle.Domain.Models.User", "User")
                        .WithMany("RelatedTags")
                        .HasForeignKey("UserId");

                    b.Navigation("Publication");

                    b.Navigation("Tag");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.Relation", b =>
                {
                    b.HasOne("SocialMedia_LifeCycle.Domain.Models.User", "AskedUser")
                        .WithMany("ReceivedRelations")
                        .HasForeignKey("AskedUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SocialMedia_LifeCycle.Domain.Models.User", "StarterUser")
                        .WithMany("StartedRelations")
                        .HasForeignKey("StarterUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AskedUser");

                    b.Navigation("StarterUser");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.Comment", b =>
                {
                    b.Navigation("Interactions");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.Interaction", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.Publication", b =>
                {
                    b.Navigation("Interactions");

                    b.Navigation("RelatedTags");
                });

            modelBuilder.Entity("SocialMedia_LifeCycle.Domain.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Interactions");

                    b.Navigation("Publications");

                    b.Navigation("ReceivedRelations");

                    b.Navigation("RelatedTags");

                    b.Navigation("StartedRelations");
                });
#pragma warning restore 612, 618
        }
    }
}
