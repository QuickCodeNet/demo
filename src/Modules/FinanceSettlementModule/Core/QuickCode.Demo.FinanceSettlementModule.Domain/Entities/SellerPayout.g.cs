using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.FinanceSettlementModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.FinanceSettlementModule.Domain.Enums;

namespace QuickCode.Demo.FinanceSettlementModule.Domain.Entities;

[Table("SELLER_PAYOUTS")]
public partial class SellerPayout : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SELLER_ID")]
	public int SellerId { get; set; }
	
	[Column("PAYOUT_PERIOD_ID")]
	public int PayoutPeriodId { get; set; }
	
	[Column("TOTAL_EARNINGS")]
	public decimal TotalEarnings { get; set; }
	
	[Column("TOTAL_COMMISSION")]
	public decimal TotalCommission { get; set; }
	
	[Column("NET_PAYOUT_AMOUNT")]
	public decimal NetPayoutAmount { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public PayoutStatus Status { get; set; }
	
	[Column("REQUESTED_DATE")]
	public DateTime RequestedDate { get; set; }
	
	[Column("PROCESSED_DATE")]
	public DateTime ProcessedDate { get; set; }
	
	[Column("TRANSACTION_ID")]
	[StringLength(50)]
	public string TransactionId { get; set; }
	
	[InverseProperty(nameof(PayoutLineItem.SellerPayout))]
	public virtual ICollection<PayoutLineItem> PayoutLineItems { get; } = new List<PayoutLineItem>();


	[ForeignKey("PayoutPeriodId")]
	[InverseProperty(nameof(PayoutPeriod.SellerPayouts))]
	public virtual PayoutPeriod PayoutPeriod { get; set; } = null!;

}

