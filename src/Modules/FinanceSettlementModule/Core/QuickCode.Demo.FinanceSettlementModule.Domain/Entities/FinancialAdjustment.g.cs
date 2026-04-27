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

[Table("FINANCIAL_ADJUSTMENTS")]
public partial class FinancialAdjustment : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SELLER_ID")]
	public int SellerId { get; set; }
	
	[Column("AMOUNT")]
	public decimal Amount { get; set; }
	
	[Column("REASON", TypeName = "nvarchar(250)")]
	public AdjustmentReason Reason { get; set; }
	
	[Column("NOTES")]
	[StringLength(1000)]
	public string Notes { get; set; }
	
	[Column("CREATED_BY_USER_ID")]
	public int CreatedByUserId { get; set; }
	
	[Column("CREATED_DATE")]
	public DateTime CreatedDate { get; set; }
	}

