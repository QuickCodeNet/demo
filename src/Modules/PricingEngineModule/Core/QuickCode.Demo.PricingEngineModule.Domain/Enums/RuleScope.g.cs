using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickCode.Demo.PricingEngineModule.Domain.Enums;

public enum RuleScope{
	[Description("Applies to all sellers and products")]
	Global,
	[Description("Applies to a specific seller tier")]
	SellerTier,
	[Description("Applies to a specific product category")]
	Category,
	[Description("Applies to a specific seller")]
	Seller
}
