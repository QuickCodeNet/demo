using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.ProductCatalogModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Domain.Entities;

[Table("PRODUCTS")]
public partial class Product : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SELLER_ID")]
	public int SellerId { get; set; }
	
	[Column("BRAND_ID")]
	public int BrandId { get; set; }
	
	[Column("PRIMARY_CATEGORY_ID")]
	public int PrimaryCategoryId { get; set; }
	
	[Column("SKU")]
	[StringLength(50)]
	public string Sku { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(1000)]
	public string Description { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public ProductStatus Status { get; set; }
	
	[Column("IS_FEATURED")]
	public bool IsFeatured { get; set; }
	
	[Column("CREATED_DATE")]
	public DateTime CreatedDate { get; set; }
	
	[InverseProperty(nameof(ProductVariant.Product))]
	public virtual ICollection<ProductVariant> ProductVariants { get; } = new List<ProductVariant>();


	[InverseProperty(nameof(ProductCategory.Product))]
	public virtual ICollection<ProductCategory> ProductCategories { get; } = new List<ProductCategory>();


	[ForeignKey("BrandId")]
	[InverseProperty(nameof(Brand.Products))]
	public virtual Brand Brand { get; set; } = null!;


	[ForeignKey("PrimaryCategoryId")]
	[InverseProperty(nameof(Category.Products))]
	public virtual Category Category { get; set; } = null!;

}

