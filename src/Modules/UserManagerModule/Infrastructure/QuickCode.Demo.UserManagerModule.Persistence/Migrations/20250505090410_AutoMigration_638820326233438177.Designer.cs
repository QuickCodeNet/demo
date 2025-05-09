﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickCode.Demo.UserManagerModule.Persistence.Contexts;

#nullable disable

namespace QuickCode.Demo.UserManagerModule.Persistence.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    [Migration("20250505090410_AutoMigration_638820326233438177")]
    partial class AutoMigration_638820326233438177
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.ApiMethodDefinitions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ControllerName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("ControllerName");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedOnUtc");

                    b.Property<string>("HttpMethod")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("HttpMethod");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)")
                        .HasColumnName("ItemType")
                        .HasDefaultValueSql("'m'");

                    b.Property<string>("UrlPath")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("UrlPath");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.ToTable("ApiMethodDefinitions");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.ApiPermissionGroups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApiMethodDefinitionId")
                        .HasColumnType("int")
                        .HasColumnName("ApiMethodDefinitionId");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedOnUtc");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<int>("PermissionGroupId")
                        .HasColumnType("int")
                        .HasColumnName("PermissionGroupId");

                    b.HasKey("Id");

                    b.HasIndex("ApiMethodDefinitionId");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.HasIndex("PermissionGroupId");

                    b.ToTable("ApiPermissionGroups");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetRoleClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ClaimType");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ClaimValue");

                    b.Property<string>("RoleId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetRoles", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("Name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("NormalizedName");

                    b.HasKey("Id");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUserClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ClaimType");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ClaimValue");

                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUserLogins", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("LoginProvider");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ProviderKey");

                    b.Property<string>("ProviderDisplayName")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUserRoles", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserId");

                    b.Property<string>("RoleId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUserTokens", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("LoginProvider");

                    b.Property<string>("Name")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Name");

                    b.Property<string>("Value")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Value");

                    b.HasKey("UserId");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUsers", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int")
                        .HasColumnName("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("Email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit")
                        .HasColumnName("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("LastName");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("NormalizedEmail");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("NormalizedUserName");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PasswordHash");

                    b.Property<int>("PermissionGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PermissionGroupId")
                        .HasDefaultValueSql("1");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit")
                        .HasColumnName("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit")
                        .HasColumnName("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasColumnName("UserName");

                    b.HasKey("Id");

                    b.HasIndex("PermissionGroupId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.ColumnTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedOnUtc");

                    b.Property<string>("IconCode")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("IconCode");

                    b.Property<string>("IosComponentName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("IosComponentName");

                    b.Property<string>("IosType")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("IosType");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<string>("TypeName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("TypeName");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.ToTable("ColumnTypes");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.KafkaEvents", b =>
                {
                    b.Property<string>("TopicName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("TopicName");

                    b.Property<int>("ApiMethodDefinitionId")
                        .HasColumnType("int")
                        .HasColumnName("ApiMethodDefinitionId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true)
                        .HasColumnName("IsActive");

                    b.HasKey("TopicName");

                    b.HasIndex("ApiMethodDefinitionId");

                    b.ToTable("KafkaEvents");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.PermissionGroups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedOnUtc");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.ToTable("PermissionGroups");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.PortalMenus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActionName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("ActionName");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedOnUtc");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)")
                        .HasColumnName("ItemType")
                        .HasDefaultValueSql("'m'");

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Name");

                    b.Property<int>("OrderNo")
                        .HasColumnType("int")
                        .HasColumnName("OrderNo");

                    b.Property<string>("ParentName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("ParentName");

                    b.Property<string>("Text")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Text");

                    b.Property<string>("Tooltip")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Tooltip");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.ToTable("PortalMenus");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.PortalPermissionGroups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedOnUtc");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<int>("PermissionGroupId")
                        .HasColumnType("int")
                        .HasColumnName("PermissionGroupId");

                    b.Property<int>("PortalPermissionId")
                        .HasColumnType("int")
                        .HasColumnName("PortalPermissionId");

                    b.Property<int>("PortalPermissionTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PortalPermissionTypeId")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.HasIndex("PermissionGroupId");

                    b.HasIndex("PortalPermissionId");

                    b.HasIndex("PortalPermissionTypeId");

                    b.ToTable("PortalPermissionGroups");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.PortalPermissionTypes", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("PortalPermissionTypes");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.PortalPermissions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedOnUtc");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)")
                        .HasColumnName("ItemType")
                        .HasDefaultValueSql("'m'");

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.ToTable("PortalPermissions");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.RefreshTokens", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedOnUtc");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ExpiryDate");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<bool>("IsRevoked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true)
                        .HasColumnName("IsRevoked");

                    b.Property<string>("Token")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Token");

                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.TableComboboxSettings", b =>
                {
                    b.Property<string>("TableName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("TableName");

                    b.Property<string>("IdColumn")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("IdColumn");

                    b.Property<string>("StringFormat")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("StringFormat");

                    b.Property<string>("TextColumns")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TextColumns");

                    b.HasKey("TableName");

                    b.ToTable("TableComboboxSettings");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.TopicWorkflows", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedOnUtc");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsDeleted");

                    b.Property<string>("KafkaEventsTopicName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("KafkaEventsTopicName");

                    b.Property<string>("WorkflowContent")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("WorkflowContent");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("IsDeleted = 0");

                    b.HasIndex("KafkaEventsTopicName");

                    b.ToTable("TopicWorkflows");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.ApiPermissionGroups", b =>
                {
                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.ApiMethodDefinitions", "ApiMethodDefinition")
                        .WithMany("ApiPermissionGroups")
                        .HasForeignKey("ApiMethodDefinitionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.PermissionGroups", "PermissionGroup")
                        .WithMany("ApiPermissionGroups")
                        .HasForeignKey("PermissionGroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApiMethodDefinition");

                    b.Navigation("PermissionGroup");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetRoleClaims", b =>
                {
                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetRoles", "Role")
                        .WithMany("AspNetRoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Role");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUserClaims", b =>
                {
                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUsers", "User")
                        .WithMany("AspNetUserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUserLogins", b =>
                {
                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUsers", "User")
                        .WithMany("AspNetUserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUserRoles", b =>
                {
                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetRoles", "Role")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUsers", "User")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUserTokens", b =>
                {
                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUsers", "User")
                        .WithMany("AspNetUserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUsers", b =>
                {
                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.PermissionGroups", "PermissionGroup")
                        .WithMany("AspNetUsers")
                        .HasForeignKey("PermissionGroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PermissionGroup");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.KafkaEvents", b =>
                {
                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.ApiMethodDefinitions", "ApiMethodDefinition")
                        .WithMany("KafkaEvents")
                        .HasForeignKey("ApiMethodDefinitionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApiMethodDefinition");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.PortalPermissionGroups", b =>
                {
                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.PermissionGroups", "PermissionGroup")
                        .WithMany("PortalPermissionGroups")
                        .HasForeignKey("PermissionGroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.PortalPermissions", "PortalPermission")
                        .WithMany("PortalPermissionGroups")
                        .HasForeignKey("PortalPermissionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.PortalPermissionTypes", "PortalPermissionType")
                        .WithMany("PortalPermissionGroups")
                        .HasForeignKey("PortalPermissionTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PermissionGroup");

                    b.Navigation("PortalPermission");

                    b.Navigation("PortalPermissionType");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.RefreshTokens", b =>
                {
                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUsers", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.TopicWorkflows", b =>
                {
                    b.HasOne("QuickCode.Demo.UserManagerModule.Domain.Entities.KafkaEvents", "KafkaEvents")
                        .WithMany("TopicWorkflows")
                        .HasForeignKey("KafkaEventsTopicName")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("KafkaEvents");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.ApiMethodDefinitions", b =>
                {
                    b.Navigation("ApiPermissionGroups");

                    b.Navigation("KafkaEvents");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetRoles", b =>
                {
                    b.Navigation("AspNetRoleClaims");

                    b.Navigation("AspNetUserRoles");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.AspNetUsers", b =>
                {
                    b.Navigation("AspNetUserClaims");

                    b.Navigation("AspNetUserLogins");

                    b.Navigation("AspNetUserRoles");

                    b.Navigation("AspNetUserTokens");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.KafkaEvents", b =>
                {
                    b.Navigation("TopicWorkflows");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.PermissionGroups", b =>
                {
                    b.Navigation("ApiPermissionGroups");

                    b.Navigation("AspNetUsers");

                    b.Navigation("PortalPermissionGroups");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.PortalPermissionTypes", b =>
                {
                    b.Navigation("PortalPermissionGroups");
                });

            modelBuilder.Entity("QuickCode.Demo.UserManagerModule.Domain.Entities.PortalPermissions", b =>
                {
                    b.Navigation("PortalPermissionGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
