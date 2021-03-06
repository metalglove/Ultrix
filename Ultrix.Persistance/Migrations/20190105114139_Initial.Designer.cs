﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190105114139_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ultrix.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("TimestampCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Authentication.Credential", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CredentialTypeId");

                    b.Property<string>("Extra");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Secret")
                        .HasMaxLength(1024);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CredentialTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Authentication.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<int?>("Position");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Authentication.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<int?>("Position");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Authentication.RolePermission", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("PermissionId");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Authentication.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<DateTime>("TimestampAdded")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.CollectionItemDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddedByUserId");

                    b.Property<int>("CollectionId");

                    b.Property<string>("MemeId");

                    b.Property<DateTime>("TimestampAdded")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.HasKey("Id");

                    b.HasIndex("AddedByUserId");

                    b.HasIndex("CollectionId");

                    b.HasIndex("MemeId");

                    b.ToTable("CollectionItemDetails");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.CollectionSubscriber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CollectionId");

                    b.Property<bool>("IsAuthorized");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.HasIndex("UserId");

                    b.ToTable("CollectionSubscribers");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MemeId");

                    b.Property<string>("Text");

                    b.Property<DateTime>("TimestampAdded")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MemeId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Follower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FollowerUserId");

                    b.Property<DateTime>("TimestampFollowed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FollowerUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Followers");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Meme", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageUrl");

                    b.Property<string>("PageUrl");

                    b.Property<DateTime>("TimestampAdded")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("Title");

                    b.Property<string>("VideoUrl");

                    b.HasKey("Id");

                    b.ToTable("Memes");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.MemeLike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsLike");

                    b.Property<string>("MemeId");

                    b.Property<DateTime>("TimestampAdded")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MemeId");

                    b.HasIndex("UserId");

                    b.ToTable("MemeLikes");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.SharedMeme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsSeen");

                    b.Property<string>("MemeId");

                    b.Property<int>("ReceiverUserId");

                    b.Property<int>("SenderUserId");

                    b.Property<DateTime>("TimestampShared")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.HasKey("Id");

                    b.HasIndex("MemeId");

                    b.HasIndex("ReceiverUserId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("SharedMemes");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.UserDetail", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("ProfilePictureData");

                    b.Property<DateTime>("TimestampCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.HasKey("Id");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("Ultrix.Domain.Enumerations.CredentialType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<int?>("Position");

                    b.HasKey("Id");

                    b.ToTable("CredentialTypes");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Authentication.Credential", b =>
                {
                    b.HasOne("Ultrix.Domain.Enumerations.CredentialType", "CredentialType")
                        .WithMany("Credentials")
                        .HasForeignKey("CredentialTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ultrix.Domain.Entities.ApplicationUser", "User")
                        .WithMany("Credentials")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Authentication.RolePermission", b =>
                {
                    b.HasOne("Ultrix.Domain.Entities.Authentication.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ultrix.Domain.Entities.Authentication.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Authentication.UserRole", b =>
                {
                    b.HasOne("Ultrix.Domain.Entities.Authentication.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ultrix.Domain.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Collection", b =>
                {
                    b.HasOne("Ultrix.Domain.Entities.ApplicationUser", "User")
                        .WithMany("Collections")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.CollectionItemDetail", b =>
                {
                    b.HasOne("Ultrix.Domain.Entities.ApplicationUser", "User")
                        .WithMany("CollectionItemDetails")
                        .HasForeignKey("AddedByUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Ultrix.Domain.Entities.Collection", "Collection")
                        .WithMany("CollectionItemDetails")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ultrix.Domain.Entities.Meme", "Meme")
                        .WithMany("InCollectionItemDetails")
                        .HasForeignKey("MemeId");
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.CollectionSubscriber", b =>
                {
                    b.HasOne("Ultrix.Domain.Entities.Collection", "Collection")
                        .WithMany("CollectionSubscribers")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ultrix.Domain.Entities.ApplicationUser", "User")
                        .WithMany("CollectionSubscribers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Comment", b =>
                {
                    b.HasOne("Ultrix.Domain.Entities.Meme", "Meme")
                        .WithMany("Comments")
                        .HasForeignKey("MemeId");

                    b.HasOne("Ultrix.Domain.Entities.ApplicationUser", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.Follower", b =>
                {
                    b.HasOne("Ultrix.Domain.Entities.ApplicationUser", "FollowerUser")
                        .WithMany("Follows")
                        .HasForeignKey("FollowerUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Ultrix.Domain.Entities.ApplicationUser", "User")
                        .WithMany("Followers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.MemeLike", b =>
                {
                    b.HasOne("Ultrix.Domain.Entities.Meme", "Meme")
                        .WithMany("Likes")
                        .HasForeignKey("MemeId");

                    b.HasOne("Ultrix.Domain.Entities.ApplicationUser", "User")
                        .WithMany("MemeLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.SharedMeme", b =>
                {
                    b.HasOne("Ultrix.Domain.Entities.Meme", "Meme")
                        .WithMany("Shares")
                        .HasForeignKey("MemeId");

                    b.HasOne("Ultrix.Domain.Entities.ApplicationUser", "ReceiverUser")
                        .WithMany("ReceivedSharedMemes")
                        .HasForeignKey("ReceiverUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Ultrix.Domain.Entities.ApplicationUser", "SenderUser")
                        .WithMany("SendSharedMemes")
                        .HasForeignKey("SenderUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Ultrix.Domain.Entities.UserDetail", b =>
                {
                    b.HasOne("Ultrix.Domain.Entities.ApplicationUser", "ApplicationUser")
                        .WithOne("UserDetail")
                        .HasForeignKey("Ultrix.Domain.Entities.UserDetail", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
