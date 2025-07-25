using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("FLATS")]
public partial class Flats  : BaseSoftDeletable 
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
	public virtual ICollection<FlatContacts> FlatContacts { get; } = new List<FlatContacts>();


	[InverseProperty("Flat")]
	public virtual ICollection<FlatPayments> FlatPayments { get; } = new List<FlatPayments>();


	[InverseProperty("Flat")]
	public virtual ICollection<FlatExpenseInstallments> FlatExpenseInstallments { get; } = new List<FlatExpenseInstallments>();


	[InverseProperty("Flat")]
	public virtual ICollection<UserSiteAccesses> UserSiteAccesses { get; } = new List<UserSiteAccesses>();


	[ForeignKey("ApartmentId")]
	[InverseProperty("Flats")]
	public virtual Apartments Apartment { get; set; } = null!;


	[ForeignKey("SiteId")]
	[InverseProperty("Flats")]
	public virtual Sites Site { get; set; } = null!;

}

