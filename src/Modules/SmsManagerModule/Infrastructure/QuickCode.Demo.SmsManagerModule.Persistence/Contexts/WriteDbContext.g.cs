using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;

namespace QuickCode.Demo.SmsManagerModule.Persistence.Contexts;

public partial class WriteDbContext : DbContext
{
	public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options) { }


	public virtual DbSet<InfoMessage> InfoMessage { get; set; }

	public virtual DbSet<CampaignMessage> CampaignMessage { get; set; }

	public virtual DbSet<OtpMessage> OtpMessage { get; set; }

	public virtual DbSet<OtpType> OtpType { get; set; }

	public virtual DbSet<InfoType> InfoType { get; set; }

	public virtual DbSet<CampaignType> CampaignType { get; set; }

	public virtual DbSet<SmsSender> SmsSender { get; set; }

	public virtual DbSet<AuditLog> AuditLog { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<InfoMessage>()
		.Property(b => b.MessageDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<CampaignMessage>()
		.Property(b => b.MessageDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<OtpMessage>()
		.Property(b => b.MessageDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<AuditLog>()
		.Property(b => b.IsChanged)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<AuditLog>()
		.Property(b => b.IsSuccess)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<InfoMessage>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<InfoMessage>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<CampaignMessage>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<CampaignMessage>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<OtpMessage>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<OtpMessage>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<OtpType>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<OtpType>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<InfoType>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<InfoType>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<CampaignType>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<CampaignType>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<SmsSender>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<SmsSender>().HasQueryFilter(r => !r.IsDeleted);


		modelBuilder.Entity<InfoMessage>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<CampaignMessage>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<OtpMessage>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<OtpType>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<InfoType>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<CampaignType>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<SmsSender>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");

		OnModelCreatingPartial(modelBuilder);

		foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
		{
		    relationship.DeleteBehavior = DeleteBehavior.Restrict;
		}
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
