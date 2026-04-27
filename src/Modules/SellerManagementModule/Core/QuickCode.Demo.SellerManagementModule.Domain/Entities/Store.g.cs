using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.SellerManagementModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.SellerManagementModule.Domain.Entities;

[Table("STORES")]
public partial class Store : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SELLER_ID")]
	public int SellerId { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("SLUG")]
	[StringLength(50)]
	public string Slug { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(1000)]
	public string Description { get; set; }
	
	[Column("LOGO_URL")]
	[StringLength(500)]
	public string LogoUrl { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[ForeignKey("SellerId")]
	[InverseProperty(nameof(Seller.Stores))]
	public virtual Seller Seller { get; set; } = null!;

}

