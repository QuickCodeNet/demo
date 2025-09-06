using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("APARTMENT_FEE_PLANS")]
public partial class ApartmentFeePlans  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SITE_ID")]
	public int SiteId { get; set; }
	
	[Column("APARTMENT_ID")]
	public int ApartmentId { get; set; }
	
	[Column("YEAR_ID")]
	public int YearId { get; set; }
	
	[Column("MONTH_ID")]
	public int MonthId { get; set; }
	
	[Column("FEE_AMOUNT")]
	public decimal FeeAmount { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[InverseProperty("FeePlan")]
	public virtual ICollection<FlatPayments> FlatPayments { get; } = new List<FlatPayments>();


	[ForeignKey("SiteId")]
	[InverseProperty("ApartmentFeePlans")]
	public virtual Sites Site { get; set; } = null!;


	[ForeignKey("ApartmentId")]
	[InverseProperty("ApartmentFeePlans")]
	public virtual Apartments Apartment { get; set; } = null!;


	[ForeignKey("YearId")]
	[InverseProperty("ApartmentFeePlans")]
	public virtual PaymentYears Year { get; set; } = null!;


	[ForeignKey("MonthId")]
	[InverseProperty("ApartmentFeePlans")]
	public virtual PaymentMonths Month { get; set; } = null!;

}

