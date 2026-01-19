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

[Table("ORDERS")]
public partial class Order : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("USER_ID")]
	public int UserId { get; set; }
	
	[Column("ORDER_DATE")]
	public DateTime OrderDate { get; set; }
	
	[Column("TOTAL_AMOUNT")]
	public decimal TotalAmount { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public OrderStatus Status { get; set; }
	
	[Column("PAYMENT_ID")]
	public int PaymentId { get; set; }
	
	[Column("SHIPPING_INFO_ID")]
	public int ShippingInfoId { get; set; }
	
	[InverseProperty("Order")]
	public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();


	[InverseProperty("Order")]
	public virtual ICollection<Shipment> Shipments { get; } = new List<Shipment>();


	[ForeignKey("UserId")]
	[InverseProperty("Orders")]
	public virtual User User { get; set; } = null!;


	[ForeignKey("PaymentId")]
	[InverseProperty("Orders")]
	public virtual Payment Payment { get; set; } = null!;


	[ForeignKey("ShippingInfoId")]
	[InverseProperty("Orders")]
	public virtual ShippingInfo ShippingInfo { get; set; } = null!;

}

