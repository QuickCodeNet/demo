using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("PAYMENT_TYPES")]
public partial class PaymentTypes  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(250)]
	public string? Description { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[InverseProperty("PaymentType")]
	public virtual ICollection<FlatPayments> FlatPayments { get; } = new List<FlatPayments>();


	[InverseProperty("PaymentType")]
	public virtual ICollection<CommonExpenses> CommonExpenses { get; } = new List<CommonExpenses>();


	[InverseProperty("PaymentType")]
	public virtual ICollection<ExpenseInstallments> ExpenseInstallments { get; } = new List<ExpenseInstallments>();


	[InverseProperty("PaymentType")]
	public virtual ICollection<FlatExpenseInstallments> FlatExpenseInstallments { get; } = new List<FlatExpenseInstallments>();

}

