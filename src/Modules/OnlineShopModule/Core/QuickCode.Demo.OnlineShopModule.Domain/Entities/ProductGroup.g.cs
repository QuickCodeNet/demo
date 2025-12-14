using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.OnlineShopModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.OnlineShopModule.Domain.Entities;

[Table("PRODUCT_GROUPS")]
public partial class ProductGroup : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("TITLE")]
	[StringLength(250)]
	public string Title { get; set; }
	
	[Column("IMAGE_URL")]
	[StringLength(500)]
	public string ImageUrl { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(250)]
	public string Description { get; set; }
	
	[Column("CODE")]
	[StringLength(250)]
	public string Code { get; set; }
	
	[Column("PRODUCT_TYPE_ID")]
	public int ProductTypeId { get; set; }
	
	[InverseProperty("ProductGroup")]
	public virtual ICollection<Product> Products { get; } = new List<Product>();


	[ForeignKey("ProductTypeId")]
	[InverseProperty("ProductGroups")]
	public virtual ProductType ProductType { get; set; } = null!;

}

