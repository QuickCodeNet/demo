using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("FLAT_PAYMENTS")]
public partial class FlatPayments  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SITE_ID")]
	public int SiteId { get; set; }
	
	[Column("FLAT_ID")]
	public int FlatId { get; set; }
	
	[Column("APARTMENT_ID")]
	public int ApartmentId { get; set; }
	
	[Column("YEAR_ID")]
	public int YearId { get; set; }
	
	[Column("MONTH_ID")]
	public int MonthId { get; set; }
	
	[Column("PAYMENT_AMOUNT")]
	public decimal PaymentAmount { get; set; }
	
	[Column("FEE_TYPE_ID")]
	public int FeeTypeId { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(250)]
	public string Description { get; set; }
	
	[Column("FEE_PLAN_ID")]
	public int FeePlanId { get; set; }
	
	[Column("EXPENSE_ID")]
	public int? ExpenseId { get; set; }
	
	[Column("PAID")]
	public bool Paid { get; set; }
	
	[Column("PAID_AT")]
	public DateTime? PaidAt { get; set; }
	
	[Column("PAYMENT_TYPE_ID")]
	public int PaymentTypeId { get; set; }
	
	[Column("CREATED_DATE")]
	public DateTime CreatedDate { get; set; }
	
	[ForeignKey("SiteId")]
	[InverseProperty("FlatPayments")]
	public virtual Sites Site { get; set; } = null!;


	[ForeignKey("FlatId")]
	[InverseProperty("FlatPayments")]
	public virtual Flats Flat { get; set; } = null!;


	[ForeignKey("ApartmentId")]
	[InverseProperty("FlatPayments")]
	public virtual Apartments Apartment { get; set; } = null!;


	[ForeignKey("FeeTypeId")]
	[InverseProperty("FlatPayments")]
	public virtual FeeTypes FeeType { get; set; } = null!;


	[ForeignKey("PaymentTypeId")]
	[InverseProperty("FlatPayments")]
	public virtual PaymentTypes PaymentType { get; set; } = null!;


	[ForeignKey("YearId")]
	[InverseProperty("FlatPayments")]
	public virtual PaymentYears Year { get; set; } = null!;


	[ForeignKey("MonthId")]
	[InverseProperty("FlatPayments")]
	public virtual PaymentMonths Month { get; set; } = null!;


	[ForeignKey("FeePlanId")]
	[InverseProperty("FlatPayments")]
	public virtual ApartmentFeePlans FeePlan { get; set; } = null!;


	[ForeignKey("ExpenseId")]
	[InverseProperty("FlatPayments")]
	public virtual CommonExpenses Expense { get; set; } = null!;

}

