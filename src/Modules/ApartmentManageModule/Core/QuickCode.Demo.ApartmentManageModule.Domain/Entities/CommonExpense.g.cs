using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.ApartmentManageModule.Domain;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("COMMON_EXPENSES")]
public partial class CommonExpense  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SITE_ID")]
	public int SiteId { get; set; }
	
	[Column("APARTMENT_ID")]
	public int ApartmentId { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("EXPENSE_TYPE_ID")]
	public int ExpenseTypeId { get; set; }
	
	[Column("YEAR_ID")]
	public int YearId { get; set; }
	
	[Column("MONTH_ID")]
	public int MonthId { get; set; }
	
	[Column("EXPENSE_AMOUNT")]
	public decimal ExpenseAmount { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(250)]
	public string? Description { get; set; }
	
	[Column("PAID")]
	public bool Paid { get; set; }
	
	[Column("PAID_AT")]
	public DateTime? PaidAt { get; set; }
	
	[Column("PAYMENT_TYPE_ID")]
	public int PaymentTypeId { get; set; }
	
	[Column("CREATED_DATE")]
	public DateTime CreatedDate { get; set; }
	
	[InverseProperty("Expense")]
	public virtual ICollection<FlatPayment> FlatPayments { get; } = new List<FlatPayment>();


	[InverseProperty("Expense")]
	public virtual ICollection<ExpenseInstallment> ExpenseInstallments { get; } = new List<ExpenseInstallment>();


	[InverseProperty("Expense")]
	public virtual ICollection<FlatExpenseInstallment> FlatExpenseInstallments { get; } = new List<FlatExpenseInstallment>();


	[ForeignKey("SiteId")]
	[InverseProperty("CommonExpenses")]
	public virtual Site Site { get; set; } = null!;


	[ForeignKey("PaymentTypeId")]
	[InverseProperty("CommonExpenses")]
	public virtual PaymentType PaymentType { get; set; } = null!;


	[ForeignKey("ApartmentId")]
	[InverseProperty("CommonExpenses")]
	public virtual Apartment Apartment { get; set; } = null!;


	[ForeignKey("ExpenseTypeId")]
	[InverseProperty("CommonExpenses")]
	public virtual ExpenseType ExpenseType { get; set; } = null!;


	[ForeignKey("YearId")]
	[InverseProperty("CommonExpenses")]
	public virtual PaymentYear Year { get; set; } = null!;


	[ForeignKey("MonthId")]
	[InverseProperty("CommonExpenses")]
	public virtual PaymentMonth Month { get; set; } = null!;

}

