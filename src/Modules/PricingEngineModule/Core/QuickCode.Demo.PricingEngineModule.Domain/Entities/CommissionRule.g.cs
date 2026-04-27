using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.PricingEngineModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.PricingEngineModule.Domain.Enums;

namespace QuickCode.Demo.PricingEngineModule.Domain.Entities;

[Table("COMMISSION_RULES")]
public partial class CommissionRule : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("COMMISSION_MODEL_ID")]
	public int CommissionModelId { get; set; }
	
	[Column("SCOPE", TypeName = "nvarchar(250)")]
	public RuleScope Scope { get; set; }
	
	[Column("SCOPE_ID")]
	public int ScopeId { get; set; }
	
	[Column("COMMISSION_TYPE", TypeName = "nvarchar(250)")]
	public CommissionType CommissionType { get; set; }
	
	[Column("PERCENTAGE_RATE")]
	public decimal PercentageRate { get; set; }
	
	[Column("FIXED_AMOUNT")]
	public decimal FixedAmount { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[ForeignKey("CommissionModelId")]
	[InverseProperty(nameof(CommissionModel.CommissionRules))]
	public virtual CommissionModel CommissionModel { get; set; } = null!;

}

