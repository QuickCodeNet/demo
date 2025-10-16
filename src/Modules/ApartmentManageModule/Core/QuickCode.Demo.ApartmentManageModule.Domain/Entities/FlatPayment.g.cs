using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.ApartmentManageModule.Domain;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("FLAT_PAYMENTS")]
public partial class FlatPayment  : BaseSoftDeletable 
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
	public virtual Site Site { get; set; } = null!;


	[ForeignKey("FlatId")]
	[InverseProperty("FlatPayments")]
	public virtual Flat Flat { get; set; } = null!;


	[ForeignKey("ApartmentId")]
	[InverseProperty("FlatPayments")]
	public virtual Apartment Apartment { get; set; } = null!;


	[ForeignKey("FeeTypeId")]
	[InverseProperty("FlatPayments")]
	public virtual FeeType FeeType { get; set; } = null!;


	[ForeignKey("PaymentTypeId")]
	[InverseProperty("FlatPayments")]
	public virtual PaymentType PaymentType { get; set; } = null!;


	[ForeignKey("YearId")]
	[InverseProperty("FlatPayments")]
	public virtual PaymentYear Year { get; set; } = null!;


	[ForeignKey("MonthId")]
	[InverseProperty("FlatPayments")]
	public virtual PaymentMonth Month { get; set; } = null!;


	[ForeignKey("FeePlanId")]
	[InverseProperty("FlatPayments")]
	public virtual ApartmentFeePlan FeePlan { get; set; } = null!;


	[ForeignKey("ExpenseId")]
	[InverseProperty("FlatPayments")]
	public virtual CommonExpense Expense { get; set; } = null!;

}

