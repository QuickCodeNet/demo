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

[Table("CART_ITEMS")]
public partial class CartItem : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("CART_ID")]
	public int CartId { get; set; }
	
	[Column("PRODUCT_ID")]
	public int ProductId { get; set; }
	
	[Column("QUANTITY")]
	public int Quantity { get; set; }
	
	[Column("UNIT_PRICE")]
	public decimal UnitPrice { get; set; }
	
	[ForeignKey("CartId")]
	[InverseProperty("CartItems")]
	public virtual Cart Cart { get; set; } = null!;


	[ForeignKey("ProductId")]
	[InverseProperty("CartItems")]
	public virtual Product Product { get; set; } = null!;

}

