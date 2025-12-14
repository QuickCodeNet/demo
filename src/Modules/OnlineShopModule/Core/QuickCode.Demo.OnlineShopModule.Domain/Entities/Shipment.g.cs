using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.OnlineShopModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Domain.Entities;

[Table("SHIPMENTS")]
public partial class Shipment : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("ORDER_ID")]
	public int OrderId { get; set; }
	
	[Column("CARGO_COMPANY", TypeName = "nvarchar(250)")]
	public CargoCompany CargoCompany { get; set; }
	
	[Column("TRACKING_CODE")]
	[StringLength(250)]
	public string TrackingCode { get; set; }
	
	[Column("SHIPPED_DATE")]
	public DateTime ShippedDate { get; set; }
	
	[Column("DELIVERED_DATE")]
	public DateTime DeliveredDate { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public ShipmentStatus Status { get; set; }
	
	[Column("NOTE")]
	[StringLength(250)]
	public string Note { get; set; }
	
	[ForeignKey("OrderId")]
	[InverseProperty("Shipments")]
	public virtual Order Order { get; set; } = null!;

}

