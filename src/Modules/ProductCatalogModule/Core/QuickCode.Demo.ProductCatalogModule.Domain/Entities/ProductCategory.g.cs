using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.ProductCatalogModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.ProductCatalogModule.Domain.Entities;

[Table("PRODUCT_CATEGORIES")]
public partial class ProductCategory : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("PRODUCT_ID")]
	public int ProductId { get; set; }
	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("CATEGORY_ID")]
	public int CategoryId { get; set; }
	
	[ForeignKey("ProductId")]
	[InverseProperty(nameof(Product.ProductCategories))]
	public virtual Product Product { get; set; } = null!;


	[ForeignKey("CategoryId")]
	[InverseProperty(nameof(Category.ProductCategories))]
	public virtual Category Category { get; set; } = null!;

}

