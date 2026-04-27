using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickCode.Demo.FinanceSettlementModule.Domain.Enums;

public enum AdjustmentReason{
	[Description("Shipping fee adjustment")]
	ShippingFee,
	[Description("Bonus for marketing participation")]
	MarketingBonus,
	[Description("Adjustment from a dispute resolution")]
	DisputeResolution,
	[Description("Other miscellaneous reason")]
	Other
}
