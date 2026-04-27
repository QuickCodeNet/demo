using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.FinanceSettlementModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.FinanceSettlementModule.Domain.Entities;

[Table("COMMISSION_ENTRIES")]
public partial class CommissionEntry : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("ORDER_ID")]
	public int OrderId { get; set; }
	
	[Column("ORDER_ITEM_ID")]
	public int OrderItemId { get; set; }
	
	[Column("SELLER_ID")]
	public int SellerId { get; set; }
	
	[Column("COMMISSION_MODEL_ID")]
	public int CommissionModelId { get; set; }
	
	[Column("COMMISSION_RULE_ID")]
	public int CommissionRuleId { get; set; }
	
	[Column("ITEM_PRICE")]
	public decimal ItemPrice { get; set; }
	
	[Column("COMMISSION_AMOUNT")]
	public decimal CommissionAmount { get; set; }
	
	[Column("CALCULATED_DATE")]
	public DateTime CalculatedDate { get; set; }
	}

