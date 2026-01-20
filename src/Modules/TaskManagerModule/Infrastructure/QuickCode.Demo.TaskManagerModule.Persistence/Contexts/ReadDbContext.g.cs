using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickCode.Demo.TaskManagerModule.Domain.Entities;
using QuickCode.Demo.TaskManagerModule.Domain.Enums;

namespace QuickCode.Demo.TaskManagerModule.Persistence.Contexts;

public partial class ReadDbContext : DbContext
{
	public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options) { }


	public virtual DbSet<User> User { get; set; }

	public virtual DbSet<Task> Task { get; set; }

	public virtual DbSet<Project> Project { get; set; }

	public virtual DbSet<TaskComment> TaskComment { get; set; }

	public virtual DbSet<AuditLog> AuditLog { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(false);

		modelBuilder.Entity<Task>()
		.Property(b => b.Status)
		.IsRequired()
		.HasDefaultValueSql("'TODO'");

		modelBuilder.Entity<Task>()
		.Property(b => b.CreatedAt)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<Task>()
		.Property(b => b.UpdatedAt)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");


		var converterTaskStatus = new ValueConverter<TaskStatus, string>(
		v => v.ToString(),
		v => (TaskStatus)Enum.Parse(typeof(TaskStatus), v));

		modelBuilder.Entity<Task>()
		.Property(b => b.Status)
		.HasConversion(converterTaskStatus);

		modelBuilder.Entity<Project>()
		.Property(b => b.CreatedAt)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<Project>()
		.Property(b => b.UpdatedAt)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<TaskComment>()
		.Property(b => b.CreatedAt)
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

		modelBuilder.Entity<User>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<User>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Task>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Task>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Project>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Project>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<TaskComment>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<TaskComment>().HasQueryFilter(r => !r.IsDeleted);


		modelBuilder.Entity<User>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Task>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Project>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<TaskComment>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");


		modelBuilder.Entity<Task>()
			.HasOne(e => e.User)
			.WithMany(p => p.Tasks)
			.HasForeignKey(e => e.UserId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Project>()
			.HasOne(e => e.User)
			.WithMany(p => p.Projects)
			.HasForeignKey(e => e.UserId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<TaskComment>()
			.HasOne(e => e.Task)
			.WithMany(p => p.TaskComments)
			.HasForeignKey(e => e.TaskId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<TaskComment>()
			.HasOne(e => e.User)
			.WithMany(p => p.TaskComments)
			.HasForeignKey(e => e.UserId)
			.OnDelete(DeleteBehavior.Restrict);

		OnModelCreatingPartial(modelBuilder);
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
