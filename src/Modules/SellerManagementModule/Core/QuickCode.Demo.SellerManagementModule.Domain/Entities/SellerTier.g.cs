using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.SellerManagementModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.SellerManagementModule.Domain.Entities;

[Table("SELLER_TIERS")]
public partial class SellerTier : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("MIN_SALES_VOLUME")]
	public decimal MinSalesVolume { get; set; }
	
	[Column("BENEFITS_DESCRIPTION")]
	[StringLength(1000)]
	public string BenefitsDescription { get; set; }
	
	[InverseProperty(nameof(Seller.SellerTier))]
	public virtual ICollection<Seller> Sellers { get; } = new List<Seller>();

}

