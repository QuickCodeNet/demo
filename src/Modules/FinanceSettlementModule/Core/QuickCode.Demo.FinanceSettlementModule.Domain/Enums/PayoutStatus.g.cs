using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickCode.Demo.FinanceSettlementModule.Domain.Enums;

public enum PayoutStatus{
	[Description("Payout calculated and awaiting approval")]
	Pending,
	[Description("Payout approved and sent to payment provider")]
	Processing,
	[Description("Payout successfully transferred to the seller")]
	Paid,
	[Description("Payout transfer failed")]
	Failed
}
