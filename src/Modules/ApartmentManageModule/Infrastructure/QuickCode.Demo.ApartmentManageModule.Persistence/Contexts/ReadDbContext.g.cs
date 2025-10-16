using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Persistence.Contexts;

public partial class ReadDbContext : DbContext
{
	public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options) { }


	public virtual DbSet<Site> Site { get; set; }

	public virtual DbSet<ExpenseType> ExpenseType { get; set; }

	public virtual DbSet<FeeType> FeeType { get; set; }

	public virtual DbSet<PaymentType> PaymentType { get; set; }

	public virtual DbSet<Contact> Contact { get; set; }

	public virtual DbSet<FlatContact> FlatContact { get; set; }

	public virtual DbSet<SiteManager> SiteManager { get; set; }

	public virtual DbSet<PaymentMonth> PaymentMonth { get; set; }

	public virtual DbSet<PaymentYear> PaymentYear { get; set; }

	public virtual DbSet<Apartment> Apartment { get; set; }

	public virtual DbSet<Flat> Flat { get; set; }

	public virtual DbSet<ApartmentFeePlan> ApartmentFeePlan { get; set; }

	public virtual DbSet<FlatPayment> FlatPayment { get; set; }

	public virtual DbSet<CommonExpense> CommonExpense { get; set; }

	public virtual DbSet<ExpenseInstallment> ExpenseInstallment { get; set; }

	public virtual DbSet<FlatExpenseInstallment> FlatExpenseInstallment { get; set; }

	public virtual DbSet<UserSiteAccess> UserSiteAccess { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Site>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<Site>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<ExpenseType>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<FeeType>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<PaymentType>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<Contact>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<Contact>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<FlatContact>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<FlatContact>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");


		var converterFlatContactRelationshipType = new ValueConverter<RelationshipType, string>(
		v => v.ToString(),
		v => (RelationshipType)Enum.Parse(typeof(RelationshipType), v));

		modelBuilder.Entity<FlatContact>()
		.Property(b => b.RelationshipType)
		.HasConversion(converterFlatContactRelationshipType);

		modelBuilder.Entity<SiteManager>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<SiteManager>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<Apartment>()
		.Property(b => b.TotalFlats)
		.IsRequired()
		.HasDefaultValueSql("0");

		modelBuilder.Entity<Apartment>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<Apartment>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<Flat>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<Flat>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<ApartmentFeePlan>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<FlatPayment>()
		.Property(b => b.Paid)
		.IsRequired()
		.HasDefaultValue(false);

		modelBuilder.Entity<FlatPayment>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<CommonExpense>()
		.Property(b => b.Paid)
		.IsRequired()
		.HasDefaultValue(false);

		modelBuilder.Entity<CommonExpense>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<ExpenseInstallment>()
		.Property(b => b.Paid)
		.IsRequired()
		.HasDefaultValue(false);

		modelBuilder.Entity<ExpenseInstallment>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<FlatExpenseInstallment>()
		.Property(b => b.Paid)
		.IsRequired()
		.HasDefaultValue(false);

		modelBuilder.Entity<FlatExpenseInstallment>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<UserSiteAccess>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<UserSiteAccess>()
		.Property(b => b.GrantedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");


		var converterUserSiteAccessAccessType = new ValueConverter<AccessType, string>(
		v => v.ToString(),
		v => (AccessType)Enum.Parse(typeof(AccessType), v));

		modelBuilder.Entity<UserSiteAccess>()
		.Property(b => b.AccessType)
		.HasConversion(converterUserSiteAccessAccessType);

		modelBuilder.Entity<Site>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Site>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<ExpenseType>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<ExpenseType>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<FeeType>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<FeeType>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<PaymentType>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<PaymentType>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Contact>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Contact>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<FlatContact>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<FlatContact>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<SiteManager>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<SiteManager>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Apartment>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Apartment>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Flat>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Flat>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<ApartmentFeePlan>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<ApartmentFeePlan>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<FlatPayment>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<FlatPayment>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<CommonExpense>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<CommonExpense>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<ExpenseInstallment>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<ExpenseInstallment>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<FlatExpenseInstallment>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<FlatExpenseInstallment>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<UserSiteAccess>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<UserSiteAccess>().HasQueryFilter(r => !r.IsDeleted);


		modelBuilder.Entity<Site>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<ExpenseType>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<FeeType>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<PaymentType>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Contact>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<FlatContact>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<SiteManager>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Apartment>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Flat>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<ApartmentFeePlan>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<FlatPayment>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<CommonExpense>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<ExpenseInstallment>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<FlatExpenseInstallment>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<UserSiteAccess>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");

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
