using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.ProductCatalogModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.ProductCatalogModule.Domain.Entities;

[Table("ATTRIBUTE_VALUES")]
public partial class AttributeValue : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("ATTRIBUTE_ID")]
	public int AttributeId { get; set; }
	
	[Column("VALUE")]
	[StringLength(250)]
	public string Value { get; set; }
	
	[InverseProperty(nameof(ProductVariantAttribute.AttributeValue))]
	public virtual ICollection<ProductVariantAttribute> ProductVariantAttributes { get; } = new List<ProductVariantAttribute>();


	[ForeignKey("AttributeId")]
	[InverseProperty(nameof(Attribute.AttributeValues))]
	public virtual Attribute Attribute { get; set; } = null!;

}

