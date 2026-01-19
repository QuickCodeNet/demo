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

[Table("PAYMENTS")]
public partial class Payment : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("USER_ID")]
	public int UserId { get; set; }
	
	[Column("ORDER_ID")]
	public int OrderId { get; set; }
	
	[Column("PAYMENT_DATE")]
	public DateTime PaymentDate { get; set; }
	
	[Column("PAYMENT_METHOD", TypeName = "nvarchar(250)")]
	public PaymentMethod PaymentMethod { get; set; }
	
	[Column("TRANSACTION_ID")]
	[StringLength(250)]
	public string TransactionId { get; set; }
	
	[Column("AMOUNT")]
	public decimal Amount { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public PaymentStatus Status { get; set; }
	
	[InverseProperty("Payment")]
	public virtual ICollection<Order> Orders { get; } = new List<Order>();

}

