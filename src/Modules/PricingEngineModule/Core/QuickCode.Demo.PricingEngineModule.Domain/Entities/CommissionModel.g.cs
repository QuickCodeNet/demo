using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.PricingEngineModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.PricingEngineModule.Domain.Entities;

[Table("COMMISSION_MODELS")]
public partial class CommissionModel : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(1000)]
	public string Description { get; set; }
	
	[InverseProperty(nameof(CommissionRule.CommissionModel))]
	public virtual ICollection<CommissionRule> CommissionRules { get; } = new List<CommissionRule>();


	[InverseProperty(nameof(SellerCommissionAssignment.CommissionModel))]
	public virtual ICollection<SellerCommissionAssignment> SellerCommissionAssignments { get; } = new List<SellerCommissionAssignment>();


	[InverseProperty(nameof(CategoryCommissionOverride.CommissionModel))]
	public virtual ICollection<CategoryCommissionOverride> CategoryCommissionOverrides { get; } = new List<CategoryCommissionOverride>();

}

