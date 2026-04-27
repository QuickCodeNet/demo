using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickCode.Demo.PricingEngineModule.Domain.Enums;

public enum CommissionType{
	[Description("A percentage of the transaction value")]
	Percentage,
	[Description("A flat fee per transaction")]
	FixedAmount,
	[Description("A combination of percentage and fixed amount")]
	Mixed
}
