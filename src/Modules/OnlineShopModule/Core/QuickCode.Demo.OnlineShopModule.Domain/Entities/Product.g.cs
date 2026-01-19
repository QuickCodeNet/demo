using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.OnlineShopModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Domain.Entities;

[Table("PRODUCTS")]
public partial class Product : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("PRODUCT_GROUP_ID")]
	public int ProductGroupId { get; set; }
	
	[Column("MODEL")]
	[StringLength(250)]
	public string Model { get; set; }
	
	[Column("TITLE")]
	[StringLength(250)]
	public string Title { get; set; }
	
	[Column("DETAILS")]
	[StringLength(int.MaxValue)]
	public string Details { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(int.MaxValue)]
	public string Description { get; set; }
	
	[Column("OPTIONS")]
	[StringLength(int.MaxValue)]
	public string Options { get; set; }
	
	[Column("PRICE")]
	public decimal Price { get; set; }
	
	[Column("CURRENCY", TypeName = "nvarchar(250)")]
	public Currency Currency { get; set; }
	
	[Column("STOCK_QUANTITY")]
	public int StockQuantity { get; set; }
	
	[InverseProperty("Product")]
	public virtual ICollection<ProductImage> ProductImages { get; } = new List<ProductImage>();


	[InverseProperty("Product")]
	public virtual ICollection<ProductReview> ProductReviews { get; } = new List<ProductReview>();


	[InverseProperty("Product")]
	public virtual ICollection<CartItem> CartItems { get; } = new List<CartItem>();


	[InverseProperty("Product")]
	public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();


	[ForeignKey("ProductGroupId")]
	[InverseProperty("Products")]
	public virtual ProductGroup ProductGroup { get; set; } = null!;

}

