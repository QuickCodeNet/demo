using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.ApartmentManageModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("EXPENSE_INSTALLMENTS")]
public partial class ExpenseInstallment : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SITE_ID")]
	public int SiteId { get; set; }
	
	[Column("APARTMENT_ID")]
	public int ApartmentId { get; set; }
	
	[Column("EXPENSE_ID")]
	public int ExpenseId { get; set; }
	
	[Column("INSTALLMENT_NUMBER")]
	public int InstallmentNumber { get; set; }
	
	[Column("TOTAL_INSTALLMENTS")]
	public int TotalInstallments { get; set; }
	
	[Column("INSTALLMENT_AMOUNT")]
	public decimal InstallmentAmount { get; set; }
	
	[Column("DUE_DATE")]
	public DateTime DueDate { get; set; }
	
	[Column("PAID")]
	public bool Paid { get; set; }
	
	[Column("PAID_AT")]
	public DateTime? PaidAt { get; set; }
	
	[Column("PAYMENT_TYPE_ID")]
	public int PaymentTypeId { get; set; }
	
	[Column("CREATED_DATE")]
	public DateTime CreatedDate { get; set; }
	
	[InverseProperty("ExpenseInstallment")]
	public virtual ICollection<FlatExpenseInstallment> FlatExpenseInstallments { get; } = new List<FlatExpenseInstallment>();


	[ForeignKey("SiteId")]
	[InverseProperty("ExpenseInstallments")]
	public virtual Site Site { get; set; } = null!;


	[ForeignKey("ApartmentId")]
	[InverseProperty("ExpenseInstallments")]
	public virtual Apartment Apartment { get; set; } = null!;


	[ForeignKey("ExpenseId")]
	[InverseProperty("ExpenseInstallments")]
	public virtual CommonExpense Expense { get; set; } = null!;


	[ForeignKey("PaymentTypeId")]
	[InverseProperty("ExpenseInstallments")]
	public virtual PaymentType PaymentType { get; set; } = null!;

}

