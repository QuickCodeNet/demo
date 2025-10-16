using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.ApartmentManageModule.Domain;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("SITES")]
public partial class Site  : BaseSoftDeletable 
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
	public virtual ICollection<Apartment> Apartments { get; } = new List<Apartment>();


	[InverseProperty("Site")]
	public virtual ICollection<Flat> Flats { get; } = new List<Flat>();


	[InverseProperty("Site")]
	public virtual ICollection<SiteManager> SiteManagers { get; } = new List<SiteManager>();


	[InverseProperty("Site")]
	public virtual ICollection<FlatPayment> FlatPayments { get; } = new List<FlatPayment>();


	[InverseProperty("Site")]
	public virtual ICollection<CommonExpense> CommonExpenses { get; } = new List<CommonExpense>();


	[InverseProperty("Site")]
	public virtual ICollection<ApartmentFeePlan> ApartmentFeePlans { get; } = new List<ApartmentFeePlan>();


	[InverseProperty("Site")]
	public virtual ICollection<ExpenseInstallment> ExpenseInstallments { get; } = new List<ExpenseInstallment>();


	[InverseProperty("Site")]
	public virtual ICollection<FlatExpenseInstallment> FlatExpenseInstallments { get; } = new List<FlatExpenseInstallment>();


	[InverseProperty("Site")]
	public virtual ICollection<UserSiteAccess> UserSiteAccesses { get; } = new List<UserSiteAccess>();

}

