using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("FLAT_EXPENSE_INSTALLMENTS")]
public partial class FlatExpenseInstallments  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SITE_ID")]
	public int SiteId { get; set; }
	
	[Column("APARTMENT_ID")]
	public int ApartmentId { get; set; }
	
	[Column("FLAT_ID")]
	public int FlatId { get; set; }
	
	[Column("EXPENSE_ID")]
	public int ExpenseId { get; set; }
	
	[Column("EXPENSE_INSTALLMENT_ID")]
	public int ExpenseInstallmentId { get; set; }
	
	[Column("FLAT_INSTALLMENT_NUMBER")]
	public int FlatInstallmentNumber { get; set; }
	
	[Column("TOTAL_FLAT_INSTALLMENTS")]
	public int TotalFlatInstallments { get; set; }
	
	[Column("FLAT_INSTALLMENT_AMOUNT")]
	public decimal FlatInstallmentAmount { get; set; }
	
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
	
	[ForeignKey("SiteId")]
	[InverseProperty("FlatExpenseInstallments")]
	public virtual Sites Site { get; set; } = null!;


	[ForeignKey("PaymentTypeId")]
	[InverseProperty("FlatExpenseInstallments")]
	public virtual PaymentTypes PaymentType { get; set; } = null!;


	[ForeignKey("ApartmentId")]
	[InverseProperty("FlatExpenseInstallments")]
	public virtual Apartments Apartment { get; set; } = null!;


	[ForeignKey("FlatId")]
	[InverseProperty("FlatExpenseInstallments")]
	public virtual Flats Flat { get; set; } = null!;


	[ForeignKey("ExpenseId")]
	[InverseProperty("FlatExpenseInstallments")]
	public virtual CommonExpenses Expense { get; set; } = null!;


	[ForeignKey("ExpenseInstallmentId")]
	[InverseProperty("FlatExpenseInstallments")]
	public virtual ExpenseInstallments ExpenseInstallment { get; set; } = null!;

}

