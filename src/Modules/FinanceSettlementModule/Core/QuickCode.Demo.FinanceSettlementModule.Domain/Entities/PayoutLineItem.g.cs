using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.FinanceSettlementModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.FinanceSettlementModule.Domain.Entities;

[Table("PAYOUT_LINE_ITEMS")]
public partial class PayoutLineItem : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("PAYOUT_ID")]
	public int PayoutId { get; set; }
	
	[Column("ORDER_ID")]
	public int OrderId { get; set; }
	
	[Column("ITEM_AMOUNT")]
	public decimal ItemAmount { get; set; }
	
	[Column("COMMISSION_AMOUNT")]
	public decimal CommissionAmount { get; set; }
	
	[Column("NET_AMOUNT")]
	public decimal NetAmount { get; set; }
	
	[ForeignKey("PayoutId")]
	[InverseProperty(nameof(SellerPayout.PayoutLineItems))]
	public virtual SellerPayout SellerPayout { get; set; } = null!;

}

