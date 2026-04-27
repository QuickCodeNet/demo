using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickCode.Demo.OrderManagementModule.Domain.Enums;

public enum ShipmentStatus{
	[Description("Shipment created, awaiting pickup")]
	Pending,
	[Description("Shipment is on its way to the customer")]
	InTransit,
	[Description("Shipment has been delivered")]
	Delivered,
	[Description("Delivery attempt failed")]
	FailedDelivery
}
