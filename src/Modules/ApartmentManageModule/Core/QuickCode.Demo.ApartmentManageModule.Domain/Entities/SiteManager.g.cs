using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.ApartmentManageModule.Domain;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("SITE_MANAGERS")]
public partial class SiteManager  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SITE_ID")]
	public int SiteId { get; set; }
	
	[Column("CONTACT_ID")]
	public int ContactId { get; set; }
	
	[Column("START_DATE")]
	public DateTime StartDate { get; set; }
	
	[Column("END_DATE")]
	public DateTime? EndDate { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[Column("CREATED_DATE")]
	public DateTime CreatedDate { get; set; }
	
	[ForeignKey("SiteId")]
	[InverseProperty("SiteManagers")]
	public virtual Site Site { get; set; } = null!;


	[ForeignKey("ContactId")]
	[InverseProperty("SiteManagers")]
	public virtual Contact Contact { get; set; } = null!;

}

