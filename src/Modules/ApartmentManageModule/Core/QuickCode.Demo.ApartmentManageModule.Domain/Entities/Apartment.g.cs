using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.ApartmentManageModule.Domain;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("APARTMENTS")]
public partial class Apartment  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SITE_ID")]
	public int SiteId { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("ADDRESS")]
	[StringLength(250)]
	public string Address { get; set; }
	
	[Column("TOTAL_FLATS")]
	public int TotalFlats { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[Column("CREATED_DATE")]
	public DateTime CreatedDate { get; set; }
	
	[InverseProperty("Apartment")]
	public virtual ICollection<Flat> Flats { get; } = new List<Flat>();


	[InverseProperty("Apartment")]
	public virtual ICollection<FlatPayment> FlatPayments { get; } = new List<FlatPayment>();


	[InverseProperty("Apartment")]
	public virtual ICollection<CommonExpense> CommonExpenses { get; } = new List<CommonExpense>();


	[InverseProperty("Apartment")]
	public virtual ICollection<ApartmentFeePlan> ApartmentFeePlans { get; } = new List<ApartmentFeePlan>();


	[InverseProperty("Apartment")]
	public virtual ICollection<ExpenseInstallment> ExpenseInstallments { get; } = new List<ExpenseInstallment>();


	[InverseProperty("Apartment")]
	public virtual ICollection<FlatExpenseInstallment> FlatExpenseInstallments { get; } = new List<FlatExpenseInstallment>();


	[ForeignKey("SiteId")]
	[InverseProperty("Apartments")]
	public virtual Site Site { get; set; } = null!;

}

