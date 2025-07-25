using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("EXPENSE_INSTALLMENTS")]
public partial class ExpenseInstallments  : BaseSoftDeletable 
{
	[Key]
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
	public virtual ICollection<FlatExpenseInstallments> FlatExpenseInstallments { get; } = new List<FlatExpenseInstallments>();


	[ForeignKey("SiteId")]
	[InverseProperty("ExpenseInstallments")]
	public virtual Sites Site { get; set; } = null!;


	[ForeignKey("ApartmentId")]
	[InverseProperty("ExpenseInstallments")]
	public virtual Apartments Apartment { get; set; } = null!;


	[ForeignKey("ExpenseId")]
	[InverseProperty("ExpenseInstallments")]
	public virtual CommonExpenses Expense { get; set; } = null!;


	[ForeignKey("PaymentTypeId")]
	[InverseProperty("ExpenseInstallments")]
	public virtual PaymentTypes PaymentType { get; set; } = null!;

}

