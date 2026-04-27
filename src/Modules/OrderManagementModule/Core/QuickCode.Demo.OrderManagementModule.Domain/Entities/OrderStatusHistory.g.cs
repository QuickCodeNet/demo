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

[Table("ORDER_STATUS_HISTORIES")]
public partial class OrderStatusHistory : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("ORDER_ID")]
	public int OrderId { get; set; }
	
	[Column("PREVIOUS_STATUS", TypeName = "nvarchar(250)")]
	public OrderStatus PreviousStatus { get; set; }
	
	[Column("NEW_STATUS", TypeName = "nvarchar(250)")]
	public OrderStatus NewStatus { get; set; }
	
	[Column("CHANGED_BY_USER_ID")]
	public int ChangedByUserId { get; set; }
	
	[Column("CHANGE_DATE")]
	public DateTime ChangeDate { get; set; }
	
	[Column("NOTES")]
	[StringLength(1000)]
	public string Notes { get; set; }
	
	[ForeignKey("OrderId")]
	[InverseProperty(nameof(Order.OrderStatusHistories))]
	public virtual Order Order { get; set; } = null!;

}

