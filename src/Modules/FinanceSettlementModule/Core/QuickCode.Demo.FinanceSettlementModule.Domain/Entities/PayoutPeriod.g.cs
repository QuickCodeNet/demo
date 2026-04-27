using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.FinanceSettlementModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.FinanceSettlementModule.Domain.Entities;

[Table("PAYOUT_PERIODS")]
public partial class PayoutPeriod : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("START_DATE")]
	public DateTime StartDate { get; set; }
	
	[Column("END_DATE")]
	public DateTime EndDate { get; set; }
	
	[Column("IS_CLOSED")]
	public bool IsClosed { get; set; }
	
	[InverseProperty(nameof(SellerPayout.PayoutPeriod))]
	public virtual ICollection<SellerPayout> SellerPayouts { get; } = new List<SellerPayout>();

}

