using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickCode.Demo.OrderManagementModule.Domain.Enums;

public enum ReturnStatus{
	[Description("Return request submitted")]
	Requested,
	[Description("Return request approved")]
	Approved,
	[Description("Return request rejected")]
	Rejected,
	[Description("Item shipped back by customer")]
	ShippedByCustomer,
	[Description("Returned item received by seller")]
	Received,
	[Description("Return process is complete")]
	Completed
}
