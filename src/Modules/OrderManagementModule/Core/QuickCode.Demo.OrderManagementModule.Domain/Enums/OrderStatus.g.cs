using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickCode.Demo.OrderManagementModule.Domain.Enums;

public enum OrderStatus{
	[Description("Awaiting payment confirmation")]
	PendingPayment,
	[Description("Payment received, order is being prepared by the seller")]
	Processing,
	[Description("Order has been shipped to the customer")]
	Shipped,
	[Description("Order has been delivered to the customer")]
	Delivered,
	[Description("Order fulfilled and transaction is complete")]
	Completed,
	[Description("Order has been cancelled")]
	Cancelled,
	[Description("Customer has requested a return")]
	ReturnRequested
}
