using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.ProductCatalogModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.ProductCatalogModule.Domain.Entities;

[Table("PRODUCT_VARIANTS")]
public partial class ProductVariant : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("PRODUCT_ID")]
	public int ProductId { get; set; }
	
	[Column("SKU")]
	[StringLength(50)]
	public string Sku { get; set; }
	
	[Column("PRICE")]
	public decimal Price { get; set; }
	
	[Column("STOCK_QUANTITY")]
	public int StockQuantity { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[InverseProperty(nameof(ProductVariantAttribute.ProductVariant))]
	public virtual ICollection<ProductVariantAttribute> ProductVariantAttributes { get; } = new List<ProductVariantAttribute>();


	[ForeignKey("ProductId")]
	[InverseProperty(nameof(Product.ProductVariants))]
	public virtual Product Product { get; set; } = null!;

}

