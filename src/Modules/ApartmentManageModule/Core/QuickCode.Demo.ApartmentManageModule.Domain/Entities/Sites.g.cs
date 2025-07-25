using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("SITES")]
public partial class Sites  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("ADDRESS")]
	[StringLength(250)]
	public string Address { get; set; }
	
	[Column("CITY")]
	[StringLength(250)]
	public string City { get; set; }
	
	[Column("DISTRICT")]
	[StringLength(250)]
	public string District { get; set; }
	
	[Column("POSTAL_CODE")]
	[StringLength(250)]
	public string PostalCode { get; set; }
	
	[Column("COUNTRY")]
	[StringLength(250)]
	public string Country { get; set; }
	
	[Column("PHONE")]
	[StringLength(250)]
	public string? Phone { get; set; }
	
	[Column("EMAIL")]
	[StringLength(250)]
	public string? Email { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[Column("CREATED_DATE")]
	public DateTime CreatedDate { get; set; }
	
	[InverseProperty("Site")]
	public virtual ICollection<Apartments> Apartments { get; } = new List<Apartments>();


	[InverseProperty("Site")]
	public virtual ICollection<Flats> Flats { get; } = new List<Flats>();


	[InverseProperty("Site")]
	public virtual ICollection<SiteManagers> SiteManagers { get; } = new List<SiteManagers>();


	[InverseProperty("Site")]
	public virtual ICollection<FlatPayments> FlatPayments { get; } = new List<FlatPayments>();


	[InverseProperty("Site")]
	public virtual ICollection<CommonExpenses> CommonExpenses { get; } = new List<CommonExpenses>();


	[InverseProperty("Site")]
	public virtual ICollection<ApartmentFeePlans> ApartmentFeePlans { get; } = new List<ApartmentFeePlans>();


	[InverseProperty("Site")]
	public virtual ICollection<ExpenseInstallments> ExpenseInstallments { get; } = new List<ExpenseInstallments>();


	[InverseProperty("Site")]
	public virtual ICollection<FlatExpenseInstallments> FlatExpenseInstallments { get; } = new List<FlatExpenseInstallments>();


	[InverseProperty("Site")]
	public virtual ICollection<UserSiteAccesses> UserSiteAccesses { get; } = new List<UserSiteAccesses>();

}

