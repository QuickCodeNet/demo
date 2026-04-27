using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.ProductCatalogModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.ProductCatalogModule.Domain.Entities;

[Table("PRODUCT_VARIANT_ATTRIBUTES")]
public partial class ProductVariantAttribute : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("VARIANT_ID")]
	public int VariantId { get; set; }
	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("ATTRIBUTE_VALUE_ID")]
	public int AttributeValueId { get; set; }
	
	[ForeignKey("VariantId")]
	[InverseProperty(nameof(ProductVariant.ProductVariantAttributes))]
	public virtual ProductVariant ProductVariant { get; set; } = null!;


	[ForeignKey("AttributeValueId")]
	[InverseProperty(nameof(AttributeValue.ProductVariantAttributes))]
	public virtual AttributeValue AttributeValue { get; set; } = null!;

}

