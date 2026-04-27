using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.OrderManagementModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.OrderManagementModule.Domain.Entities;

[Table("ORDER_ITEMS")]
public partial class OrderItem : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("ORDER_ID")]
	public int OrderId { get; set; }
	
	[Column("PRODUCT_VARIANT_ID")]
	public int ProductVariantId { get; set; }
	
	[Column("PRODUCT_NAME")]
	[StringLength(250)]
	public string ProductName { get; set; }
	
	[Column("SKU")]
	[StringLength(50)]
	public string Sku { get; set; }
	
	[Column("QUANTITY")]
	public int Quantity { get; set; }
	
	[Column("UNIT_PRICE")]
	public decimal UnitPrice { get; set; }
	
	[Column("TOTAL_PRICE")]
	public decimal TotalPrice { get; set; }
	
	[InverseProperty(nameof(ReturnRequest.OrderItem))]
	public virtual ICollection<ReturnRequest> ReturnRequests { get; } = new List<ReturnRequest>();


	[ForeignKey("OrderId")]
	[InverseProperty(nameof(Order.OrderItems))]
	public virtual Order Order { get; set; } = null!;

}

