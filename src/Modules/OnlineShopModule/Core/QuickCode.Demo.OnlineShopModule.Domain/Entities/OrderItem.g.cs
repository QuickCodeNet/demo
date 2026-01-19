using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.OnlineShopModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.OnlineShopModule.Domain.Entities;

[Table("ORDER_ITEMS")]
public partial class OrderItem : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("ORDER_ID")]
	public int OrderId { get; set; }
	
	[Column("PRODUCT_ID")]
	public int ProductId { get; set; }
	
	[Column("QUANTITY")]
	public int Quantity { get; set; }
	
	[Column("UNIT_PRICE")]
	public decimal UnitPrice { get; set; }
	
	[ForeignKey("OrderId")]
	[InverseProperty("OrderItems")]
	public virtual Order Order { get; set; } = null!;


	[ForeignKey("ProductId")]
	[InverseProperty("OrderItems")]
	public virtual Product Product { get; set; } = null!;

}

