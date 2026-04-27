using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickCode.Demo.FinanceSettlementModule.Domain.Enums;

public enum TransactionType{
	[Description("Revenue from a sale")]
	Sale,
	[Description("Deduction due to a customer refund")]
	Refund,
	[Description("Marketplace commission fee")]
	Commission,
	[Description("A manual credit adjustment")]
	AdjustmentCredit,
	[Description("A manual debit adjustment")]
	AdjustmentDebit,
	[Description("A withdrawal of funds by the seller")]
	Payout
}
