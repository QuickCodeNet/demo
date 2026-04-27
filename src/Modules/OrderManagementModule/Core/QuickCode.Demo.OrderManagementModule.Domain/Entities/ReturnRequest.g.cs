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

[Table("RETURN_REQUESTS")]
public partial class ReturnRequest : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("ORDER_ID")]
	public int OrderId { get; set; }
	
	[Column("ORDER_ITEM_ID")]
	public int OrderItemId { get; set; }
	
	[Column("REASON")]
	[StringLength(1000)]
	public string Reason { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public ReturnStatus Status { get; set; }
	
	[Column("REQUESTED_DATE")]
	public DateTime RequestedDate { get; set; }
	
	[Column("UPDATED_DATE")]
	public DateTime UpdatedDate { get; set; }
	
	[ForeignKey("OrderId")]
	[InverseProperty(nameof(Order.ReturnRequests))]
	public virtual Order Order { get; set; } = null!;


	[ForeignKey("OrderItemId")]
	[InverseProperty(nameof(OrderItem.ReturnRequests))]
	public virtual OrderItem OrderItem { get; set; } = null!;

}

