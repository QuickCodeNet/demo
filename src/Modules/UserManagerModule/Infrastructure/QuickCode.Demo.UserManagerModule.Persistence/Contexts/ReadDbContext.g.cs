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

public partial class ReadDbContext : DbContext
{
	public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options) { }


	public virtual DbSet<PortalMenus> PortalMenus { get; set; }

	public virtual DbSet<ColumnTypes> ColumnTypes { get; set; }

	public virtual DbSet<TopicWorkflows> TopicWorkflows { get; set; }

	public virtual DbSet<KafkaEvents> KafkaEvents { get; set; }

	public virtual DbSet<PermissionGroups> PermissionGroups { get; set; }

	public virtual DbSet<PortalPermissionTypes> PortalPermissionTypes { get; set; }

	public virtual DbSet<PortalPermissions> PortalPermissions { get; set; }

	public virtual DbSet<PortalPermissionGroups> PortalPermissionGroups { get; set; }

	public virtual DbSet<ApiPermissionGroups> ApiPermissionGroups { get; set; }

	public virtual DbSet<ApiMethodDefinitions> ApiMethodDefinitions { get; set; }

	public virtual DbSet<TableComboboxSettings> TableComboboxSettings { get; set; }

	public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }

	public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

	public virtual DbSet<RefreshTokens> RefreshTokens { get; set; }

	public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }

	public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }

	public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }

	public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }

	public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<KafkaEvents>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<PortalPermissionGroups>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<ApiPermissionGroups>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);


		var converterApiMethodDefinitionsHttpMethod = new ValueConverter<HttpMethodType, string>(
		v => v.ToString(),
		v => (HttpMethodType)Enum.Parse(typeof(HttpMethodType), v));

		modelBuilder.Entity<ApiMethodDefinitions>()
		.Property(b => b.HttpMethod)
		.HasConversion(converterApiMethodDefinitionsHttpMethod);

		modelBuilder.Entity<RefreshTokens>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<RefreshTokens>()
		.Property(b => b.IsRevoked)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<ColumnTypes>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<ColumnTypes>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<TopicWorkflows>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<TopicWorkflows>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<RefreshTokens>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<RefreshTokens>().HasQueryFilter(r => !r.IsDeleted);


		modelBuilder.Entity<ColumnTypes>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<TopicWorkflows>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<RefreshTokens>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");

		OnModelCreatingPartial(modelBuilder);

		foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
		{
		    relationship.DeleteBehavior = DeleteBehavior.Restrict;
		}
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    public override int SaveChanges()
    {
        throw new InvalidOperationException("ReadDbContext is read-only.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("ReadDbContext is read-only.");
    }

}
