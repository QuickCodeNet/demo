using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.PricingEngineModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.PricingEngineModule.Domain.Entities;

[Table("SELLER_COMMISSION_ASSIGNMENTS")]
public partial class SellerCommissionAssignment : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("SELLER_ID")]
	public int SellerId { get; set; }
	
	[Column("COMMISSION_MODEL_ID")]
	public int CommissionModelId { get; set; }
	
	[Column("EFFECTIVE_DATE")]
	public DateTime EffectiveDate { get; set; }
	
	[ForeignKey("CommissionModelId")]
	[InverseProperty(nameof(CommissionModel.SellerCommissionAssignments))]
	public virtual CommissionModel CommissionModel { get; set; } = null!;

}

