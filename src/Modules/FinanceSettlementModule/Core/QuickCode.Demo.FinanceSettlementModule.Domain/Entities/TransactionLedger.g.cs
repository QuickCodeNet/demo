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

[Table("TRANSACTION_LEDGERS")]
public partial class TransactionLedger : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SELLER_ID")]
	public int SellerId { get; set; }
	
	[Column("TRANSACTION_TYPE", TypeName = "nvarchar(250)")]
	public TransactionType TransactionType { get; set; }
	
	[Column("AMOUNT")]
	public decimal Amount { get; set; }
	
	[Column("REFERENCE_ID")]
	[StringLength(50)]
	public string ReferenceId { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(1000)]
	public string Description { get; set; }
	
	[Column("TRANSACTION_DATE")]
	public DateTime TransactionDate { get; set; }
	}

