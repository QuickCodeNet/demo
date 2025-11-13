using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Persistence.Contexts;

public partial class WriteDbContext : DbContext
{
	public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options) { }


	public virtual DbSet<PortalMenu> PortalMenu { get; set; }

	public virtual DbSet<ColumnType> ColumnType { get; set; }

	public virtual DbSet<TopicWorkflow> TopicWorkflow { get; set; }

	public virtual DbSet<KafkaEvent> KafkaEvent { get; set; }

	public virtual DbSet<PermissionGroup> PermissionGroup { get; set; }

	public virtual DbSet<PortalPermissionType> PortalPermissionType { get; set; }

	public virtual DbSet<PortalPermission> PortalPermission { get; set; }

	public virtual DbSet<PortalPermissionGroup> PortalPermissionGroup { get; set; }

	public virtual DbSet<ApiPermissionGroup> ApiPermissionGroup { get; set; }

	public virtual DbSet<ApiMethodDefinition> ApiMethodDefinition { get; set; }

	public virtual DbSet<TableComboboxSetting> TableComboboxSetting { get; set; }

	public virtual DbSet<AspNetRole> AspNetRole { get; set; }

	public virtual DbSet<AspNetUser> AspNetUser { get; set; }

	public virtual DbSet<RefreshToken> RefreshToken { get; set; }

	public virtual DbSet<AspNetRoleClaim> AspNetRoleClaim { get; set; }

	public virtual DbSet<AspNetUserClaim> AspNetUserClaim { get; set; }

	public virtual DbSet<AspNetUserLogin> AspNetUserLogin { get; set; }

	public virtual DbSet<AspNetUserRole> AspNetUserRole { get; set; }

	public virtual DbSet<AspNetUserToken> AspNetUserToken { get; set; }

	public virtual DbSet<AuditLog> AuditLog { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<KafkaEvent>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<PortalPermissionGroup>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<ApiPermissionGroup>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);


		var converterApiMethodDefinitionHttpMethod = new ValueConverter<HttpMethodType, string>(
		v => v.ToString(),
		v => (HttpMethodType)Enum.Parse(typeof(HttpMethodType), v));

		modelBuilder.Entity<ApiMethodDefinition>()
		.Property(b => b.HttpMethod)
		.HasConversion(converterApiMethodDefinitionHttpMethod);

		modelBuilder.Entity<RefreshToken>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<RefreshToken>()
		.Property(b => b.IsRevoked)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<AuditLog>()
		.Property(b => b.IsChanged)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<AuditLog>()
		.Property(b => b.IsSuccess)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<ColumnType>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<ColumnType>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<TopicWorkflow>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<TopicWorkflow>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<RefreshToken>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<RefreshToken>().HasQueryFilter(r => !r.IsDeleted);


		modelBuilder.Entity<ColumnType>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<TopicWorkflow>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<RefreshToken>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");

		OnModelCreatingPartial(modelBuilder);

		foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
		{
		    relationship.DeleteBehavior = DeleteBehavior.Restrict;
		}
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
