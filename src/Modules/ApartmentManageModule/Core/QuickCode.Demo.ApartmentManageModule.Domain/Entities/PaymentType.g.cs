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

[Table("PAYMENT_TYPES")]
public partial class PaymentType : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
	public virtual ICollection<FlatPayment> FlatPayments { get; } = new List<FlatPayment>();


	[InverseProperty("PaymentType")]
	public virtual ICollection<CommonExpense> CommonExpenses { get; } = new List<CommonExpense>();


	[InverseProperty("PaymentType")]
	public virtual ICollection<ExpenseInstallment> ExpenseInstallments { get; } = new List<ExpenseInstallment>();


	[InverseProperty("PaymentType")]
	public virtual ICollection<FlatExpenseInstallment> FlatExpenseInstallments { get; } = new List<FlatExpenseInstallment>();

}

