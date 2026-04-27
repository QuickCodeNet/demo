using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickCode.Demo.SellerManagementModule.Domain.Enums;

public enum SellerStatus{
	[Description("New seller, awaiting document verification")]
	PendingVerification,
	[Description("Seller is approved and can sell on the platform")]
	Active,
	[Description("Sellers account is temporarily disabled")]
	Suspended,
	[Description("Seller application was rejected")]
	Rejected,
	[Description("Seller has closed their account")]
	Closed
}
