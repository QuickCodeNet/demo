using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("APARTMENTS")]
public partial class Apartments  : BaseSoftDeletable 
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
	public virtual ICollection<Flats> Flats { get; } = new List<Flats>();


	[InverseProperty("Apartment")]
	public virtual ICollection<FlatPayments> FlatPayments { get; } = new List<FlatPayments>();


	[InverseProperty("Apartment")]
	public virtual ICollection<CommonExpenses> CommonExpenses { get; } = new List<CommonExpenses>();


	[InverseProperty("Apartment")]
	public virtual ICollection<ApartmentFeePlans> ApartmentFeePlans { get; } = new List<ApartmentFeePlans>();


	[InverseProperty("Apartment")]
	public virtual ICollection<ExpenseInstallments> ExpenseInstallments { get; } = new List<ExpenseInstallments>();


	[InverseProperty("Apartment")]
	public virtual ICollection<FlatExpenseInstallments> FlatExpenseInstallments { get; } = new List<FlatExpenseInstallments>();


	[ForeignKey("SiteId")]
	[InverseProperty("Apartments")]
	public virtual Sites Site { get; set; } = null!;

}

