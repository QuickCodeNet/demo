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

[Table("CAMPAIGN_APPLICABILITIES")]
public partial class CampaignApplicability : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("CAMPAIGN_ID")]
	public int CampaignId { get; set; }
	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("SCOPE", TypeName = "nvarchar(250)")]
	public RuleScope Scope { get; set; }
	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("SCOPE_ID")]
	public int ScopeId { get; set; }
	
	[ForeignKey("CampaignId")]
	[InverseProperty(nameof(PromotionalCampaign.CampaignApplicabilities))]
	public virtual PromotionalCampaign PromotionalCampaign { get; set; } = null!;

}

