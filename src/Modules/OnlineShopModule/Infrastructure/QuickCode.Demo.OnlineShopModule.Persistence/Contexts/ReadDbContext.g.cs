using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Persistence.Contexts;

public partial class ReadDbContext : DbContext
{
	public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options) { }


	public virtual DbSet<User> User { get; set; }

	public virtual DbSet<Company> Company { get; set; }

	public virtual DbSet<Campaign> Campaign { get; set; }

	public virtual DbSet<Coupon> Coupon { get; set; }

	public virtual DbSet<UserCoupon> UserCoupon { get; set; }

	public virtual DbSet<ProductGroup> ProductGroup { get; set; }

	public virtual DbSet<ProductType> ProductType { get; set; }

	public virtual DbSet<Product> Product { get; set; }

	public virtual DbSet<ProductImage> ProductImage { get; set; }

	public virtual DbSet<ProductReview> ProductReview { get; set; }

	public virtual DbSet<Cart> Cart { get; set; }

	public virtual DbSet<CartItem> CartItem { get; set; }

	public virtual DbSet<Order> Order { get; set; }

	public virtual DbSet<OrderItem> OrderItem { get; set; }

	public virtual DbSet<Payment> Payment { get; set; }

	public virtual DbSet<ShippingInfo> ShippingInfo { get; set; }

	public virtual DbSet<Shipment> Shipment { get; set; }

	public virtual DbSet<CompanyInfo> CompanyInfo { get; set; }

	public virtual DbSet<HomePage> HomePage { get; set; }

	public virtual DbSet<AuditLog> AuditLog { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		var converterCompanySector = new ValueConverter<CompanySector, string>(
		v => v.ToString(),
		v => (CompanySector)Enum.Parse(typeof(CompanySector), v));

		modelBuilder.Entity<Company>()
		.Property(b => b.Sector)
		.HasConversion(converterCompanySector);


		var converterCampaignTargetAudience = new ValueConverter<TargetAudience, string>(
		v => v.ToString(),
		v => (TargetAudience)Enum.Parse(typeof(TargetAudience), v));

		modelBuilder.Entity<Campaign>()
		.Property(b => b.TargetAudience)
		.HasConversion(converterCampaignTargetAudience);


		var converterProductCurrency = new ValueConverter<Currency, string>(
		v => v.ToString(),
		v => (Currency)Enum.Parse(typeof(Currency), v));

		modelBuilder.Entity<Product>()
		.Property(b => b.Currency)
		.HasConversion(converterProductCurrency);


		var converterOrderStatus = new ValueConverter<OrderStatus, string>(
		v => v.ToString(),
		v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));

		modelBuilder.Entity<Order>()
		.Property(b => b.Status)
		.HasConversion(converterOrderStatus);


		var converterPaymentPaymentMethod = new ValueConverter<PaymentMethod, string>(
		v => v.ToString(),
		v => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), v));

		modelBuilder.Entity<Payment>()
		.Property(b => b.PaymentMethod)
		.HasConversion(converterPaymentPaymentMethod);


		var converterPaymentStatus = new ValueConverter<PaymentStatus, string>(
		v => v.ToString(),
		v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v));

		modelBuilder.Entity<Payment>()
		.Property(b => b.Status)
		.HasConversion(converterPaymentStatus);


		var converterShipmentCargoCompany = new ValueConverter<CargoCompany, string>(
		v => v.ToString(),
		v => (CargoCompany)Enum.Parse(typeof(CargoCompany), v));

		modelBuilder.Entity<Shipment>()
		.Property(b => b.CargoCompany)
		.HasConversion(converterShipmentCargoCompany);


		var converterShipmentStatus = new ValueConverter<ShipmentStatus, string>(
		v => v.ToString(),
		v => (ShipmentStatus)Enum.Parse(typeof(ShipmentStatus), v));

		modelBuilder.Entity<Shipment>()
		.Property(b => b.Status)
		.HasConversion(converterShipmentStatus);

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

		modelBuilder.Entity<Company>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Company>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Campaign>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Campaign>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Coupon>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Coupon>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<ProductGroup>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<ProductGroup>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<ProductType>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<ProductType>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Product>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Product>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<ProductImage>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<ProductImage>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<ProductReview>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<ProductReview>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Cart>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Cart>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<CartItem>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<CartItem>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Order>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Order>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<OrderItem>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<OrderItem>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Payment>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Payment>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<ShippingInfo>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<ShippingInfo>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<Shipment>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<Shipment>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<CompanyInfo>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<CompanyInfo>().HasQueryFilter(r => !r.IsDeleted);

		modelBuilder.Entity<HomePage>().Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
		modelBuilder.Entity<HomePage>().HasQueryFilter(r => !r.IsDeleted);


		modelBuilder.Entity<User>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Company>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Campaign>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Coupon>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<ProductGroup>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<ProductType>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Product>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<ProductImage>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<ProductReview>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Cart>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<CartItem>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Order>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<OrderItem>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Payment>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<ShippingInfo>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<Shipment>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<CompanyInfo>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");
		modelBuilder.Entity<HomePage>().HasIndex(r => r.IsDeleted).HasFilter("IsDeleted = 0");

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
