using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.ApartmentManageModule.Domain;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("FLATS")]
public partial class Flat  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("APARTMENT_ID")]
	public int ApartmentId { get; set; }
	
	[Column("SITE_ID")]
	public int SiteId { get; set; }
	
	[Column("FLAT_NUMBER")]
	[StringLength(250)]
	public string FlatNumber { get; set; }
	
	[Column("FLOOR")]
	public int? Floor { get; set; }
	
	[Column("SQUARE_METERS")]
	public int? SquareMeters { get; set; }
	
	[Column("ROOM_COUNT")]
	public int? RoomCount { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[Column("CREATED_DATE")]
	public DateTime CreatedDate { get; set; }
	
	[InverseProperty("Flat")]
	public virtual ICollection<FlatContact> FlatContacts { get; } = new List<FlatContact>();


	[InverseProperty("Flat")]
	public virtual ICollection<FlatPayment> FlatPayments { get; } = new List<FlatPayment>();


	[InverseProperty("Flat")]
	public virtual ICollection<FlatExpenseInstallment> FlatExpenseInstallments { get; } = new List<FlatExpenseInstallment>();


	[InverseProperty("Flat")]
	public virtual ICollection<UserSiteAccess> UserSiteAccesses { get; } = new List<UserSiteAccess>();


	[ForeignKey("ApartmentId")]
	[InverseProperty("Flats")]
	public virtual Apartment Apartment { get; set; } = null!;


	[ForeignKey("SiteId")]
	[InverseProperty("Flats")]
	public virtual Site Site { get; set; } = null!;

}

