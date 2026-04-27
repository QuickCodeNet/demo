using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.PricingEngineModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.PricingEngineModule.Domain.Entities;

[Table("PROMOTIONAL_CAMPAIGNS")]
public partial class PromotionalCampaign : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("START_DATE")]
	public DateTime StartDate { get; set; }
	
	[Column("END_DATE")]
	public DateTime EndDate { get; set; }
	
	[Column("DISCOUNT_PERCENTAGE")]
	public decimal DiscountPercentage { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[InverseProperty(nameof(CampaignApplicability.PromotionalCampaign))]
	public virtual ICollection<CampaignApplicability> CampaignApplicabilities { get; } = new List<CampaignApplicability>();

}

