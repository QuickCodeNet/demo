using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.PricingEngineModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.PricingEngineModule.Domain.Entities;

[Table("CATEGORY_COMMISSION_OVERRIDES")]
public partial class CategoryCommissionOverride : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("CATEGORY_ID")]
	public int CategoryId { get; set; }
	
	[Column("COMMISSION_MODEL_ID")]
	public int CommissionModelId { get; set; }
	
	[Column("EFFECTIVE_DATE")]
	public DateTime EffectiveDate { get; set; }
	
	[ForeignKey("CommissionModelId")]
	[InverseProperty(nameof(CommissionModel.CategoryCommissionOverrides))]
	public virtual CommissionModel CommissionModel { get; set; } = null!;

}

