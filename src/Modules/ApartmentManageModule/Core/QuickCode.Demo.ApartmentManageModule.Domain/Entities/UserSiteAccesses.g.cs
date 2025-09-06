using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("USER_SITE_ACCESSES")]
public partial class UserSiteAccesses  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("USER_ID")]
	public int UserId { get; set; }
	
	[Column("SITE_ID")]
	public int SiteId { get; set; }
	
	[Column("FLAT_ID")]
	public int? FlatId { get; set; }
	
	[Column("ACCESS_TYPE", TypeName = "nvarchar(200)")]
	public AccessType AccessType { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[Column("GRANTED_DATE")]
	public DateTime GrantedDate { get; set; }
	
	[Column("GRANTED_BY")]
	public int? GrantedBy { get; set; }
	
	[ForeignKey("SiteId")]
	[InverseProperty("UserSiteAccesses")]
	public virtual Sites Site { get; set; } = null!;


	[ForeignKey("FlatId")]
	[InverseProperty("UserSiteAccesses")]
	public virtual Flats Flat { get; set; } = null!;

}

