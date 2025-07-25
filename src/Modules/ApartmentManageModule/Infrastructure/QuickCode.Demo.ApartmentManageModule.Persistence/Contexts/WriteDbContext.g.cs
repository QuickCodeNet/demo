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

public partial class WriteDbContext : DbContext
{
	public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options) { }


	public virtual DbSet<Sites> Sites { get; set; }

	public virtual DbSet<ExpenseTypes> ExpenseTypes { get; set; }

	public virtual DbSet<FeeTypes> FeeTypes { get; set; }

	public virtual DbSet<PaymentTypes> PaymentTypes { get; set; }

	public virtual DbSet<Contacts> Contacts { get; set; }

	public virtual DbSet<FlatContacts> FlatContacts { get; set; }

	public virtual DbSet<SiteManagers> SiteManagers { get; set; }

	public virtual DbSet<PaymentMonths> PaymentMonths { get; set; }

	public virtual DbSet<PaymentYears> PaymentYears { get; set; }

	public virtual DbSet<Apartments> Apartments { get; set; }

	public virtual DbSet<Flats> Flats { get; set; }

	public virtual DbSet<ApartmentFeePlans> ApartmentFeePlans { get; set; }

	public virtual DbSet<FlatPayments> FlatPayments { get; set; }

	public virtual DbSet<CommonExpenses> CommonExpenses { get; set; }

	public virtual DbSet<ExpenseInstallments> ExpenseInstallments { get; set; }

	public virtual DbSet<FlatExpenseInstallments> FlatExpenseInstallments { get; set; }

	public virtual DbSet<UserSiteAccesses> UserSiteAccesses { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Sites>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<Sites>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<ExpenseTypes>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<FeeTypes>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<PaymentTypes>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<Contacts>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<Contacts>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<FlatContacts>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<FlatContacts>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");


		var converterFlatContactsRelationshipType = new ValueConverter<RelationshipType, string>(
		v => v.ToString(),
		v => (RelationshipType)Enum.Parse(typeof(RelationshipType), v));

		modelBuilder.Entity<FlatContacts>()
		.Property(b => b.RelationshipType)
		.HasConversion(converterFlatContactsRelationshipType);

		modelBuilder.Entity<SiteManagers>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<SiteManagers>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<Apartments>()
		.Property(b => b.TotalFlats)
		.IsRequired()
		.HasDefaultValueSql("0");

		modelBuilder.Entity<Apartments>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<Apartments>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<Flats>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<Flats>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<ApartmentFeePlans>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<FlatPayments>()
		.Property(b => b.Paid)
		.IsRequired()
		.HasDefaultValue(false);

		modelBuilder.Entity<FlatPayments>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<CommonExpenses>()
		.Property(b => b.Paid)
		.IsRequired()
		.HasDefaultValue(false);

		modelBuilder.Entity<CommonExpenses>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<ExpenseInstallments>()
		.Property(b => b.Paid)
		.IsRequired()
		.HasDefaultValue(false);

		modelBuilder.Entity<ExpenseInstallments>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<FlatExpenseInstallments>()
		.Property(b => b.Paid)
		.IsRequired()
		.HasDefaultValue(false);

		modelBuilder.Entity<FlatExpenseInstallments>()
		.Property(b => b.CreatedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");

		modelBuilder.Entity<UserSiteAccesses>()
		.Property(b => b.IsActive)
		.IsRequired()
		.HasDefaultValue(true);

		modelBuilder.Entity<UserSiteAccesses>()
		.Property(b => b.GrantedDate)
		.IsRequired()
		.HasColumnType("datetime")
		.HasDefaultValueSql("getdate()");


		var converterUserSiteAccessesAccessType = new ValueConverter<AccessType, string>(
		v => v.ToString(),
		v => (AccessType)Enum.Parse(typeof(AccessType), v));

		modelBuilder.Entity<UserSiteAccesses>()
		.Property(b => b.AccessType)
		.HasConversion(converterUserSiteAccessesAccessType);

		modelBuilder.Entity<Sites>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Sites>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<ExpenseTypes>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<ExpenseTypes>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<FeeTypes>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<FeeTypes>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<PaymentTypes>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<PaymentTypes>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Contacts>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Contacts>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<FlatContacts>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<FlatContacts>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<SiteManagers>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<SiteManagers>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Apartments>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Apartments>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Flats>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Flats>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<ApartmentFeePlans>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<ApartmentFeePlans>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<FlatPayments>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<FlatPayments>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<CommonExpenses>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<CommonExpenses>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<ExpenseInstallments>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<ExpenseInstallments>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<FlatExpenseInstallments>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<FlatExpenseInstallments>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<UserSiteAccesses>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<UserSiteAccesses>().HasQueryFilter(r => !r.IsDeleted);


		modelBuilder.Entity<Sites>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<ExpenseTypes>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<FeeTypes>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<PaymentTypes>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Contacts>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<FlatContacts>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<SiteManagers>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Apartments>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Flats>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<ApartmentFeePlans>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<FlatPayments>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<CommonExpenses>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<ExpenseInstallments>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<FlatExpenseInstallments>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<UserSiteAccesses>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");

		OnModelCreatingPartial(modelBuilder);

		foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
		{
		    relationship.DeleteBehavior = DeleteBehavior.Restrict;
		}
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
