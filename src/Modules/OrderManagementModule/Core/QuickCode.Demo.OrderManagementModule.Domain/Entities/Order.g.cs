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

[Table("ORDERS")]
public partial class Order : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("CUSTOMER_ID")]
	public int CustomerId { get; set; }
	
	[Column("SELLER_ID")]
	public int SellerId { get; set; }
	
	[Column("ORDER_NUMBER")]
	[StringLength(50)]
	public string OrderNumber { get; set; }
	
	[Column("TOTAL_AMOUNT")]
	public decimal TotalAmount { get; set; }
	
	[Column("SHIPPING_ADDRESS")]
	[StringLength(1000)]
	public string ShippingAddress { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public OrderStatus Status { get; set; }
	
	[Column("ORDER_DATE")]
	public DateTime OrderDate { get; set; }
	
	[InverseProperty(nameof(OrderItem.Order))]
	public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();


	[InverseProperty(nameof(Shipment.Order))]
	public virtual ICollection<Shipment> Shipments { get; } = new List<Shipment>();


	[InverseProperty(nameof(OrderStatusHistory.Order))]
	public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; } = new List<OrderStatusHistory>();


	[InverseProperty(nameof(ReturnRequest.Order))]
	public virtual ICollection<ReturnRequest> ReturnRequests { get; } = new List<ReturnRequest>();

}

