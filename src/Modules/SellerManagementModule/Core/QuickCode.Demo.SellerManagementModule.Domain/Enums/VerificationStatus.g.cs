using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickCode.Demo.SellerManagementModule.Domain.Enums;

public enum VerificationStatus{
	[Description("Document uploaded, awaiting review")]
	Pending,
	[Description("Document has been verified")]
	Verified,
	[Description("Document was rejected")]
	Rejected
}
