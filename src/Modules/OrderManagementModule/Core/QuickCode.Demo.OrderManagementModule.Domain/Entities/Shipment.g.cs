using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.OrderManagementModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.OrderManagementModule.Domain.Enums;

namespace QuickCode.Demo.OrderManagementModule.Domain.Entities;

[Table("SHIPMENTS")]
public partial class Shipment : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("ORDER_ID")]
	public int OrderId { get; set; }
	
	[Column("SHIPPING_METHOD_ID")]
	public int ShippingMethodId { get; set; }
	
	[Column("TRACKING_NUMBER")]
	[StringLength(50)]
	public string TrackingNumber { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public ShipmentStatus Status { get; set; }
	
	[Column("SHIPPED_DATE")]
	public DateTime ShippedDate { get; set; }
	
	[Column("DELIVERED_DATE")]
	public DateTime DeliveredDate { get; set; }
	
	[ForeignKey("OrderId")]
	[InverseProperty(nameof(Order.Shipments))]
	public virtual Order Order { get; set; } = null!;


	[ForeignKey("ShippingMethodId")]
	[InverseProperty(nameof(ShippingMethod.Shipments))]
	public virtual ShippingMethod ShippingMethod { get; set; } = null!;

}

