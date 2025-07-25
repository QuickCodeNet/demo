using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("CONTACTS")]
public partial class Contacts  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("PHONE")]
	[StringLength(250)]
	public string Phone { get; set; }
	
	[Column("EMAIL")]
	[StringLength(250)]
	public string? Email { get; set; }
	
	[Column("IDENTITY_NUMBER")]
	[StringLength(250)]
	public string? IdentityNumber { get; set; }
	
	[Column("ADDRESS")]
	[StringLength(250)]
	public string? Address { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[Column("CREATED_DATE")]
	public DateTime CreatedDate { get; set; }
	
	[InverseProperty("Contact")]
	public virtual ICollection<FlatContacts> FlatContacts { get; } = new List<FlatContacts>();


	[InverseProperty("Contact")]
	public virtual ICollection<SiteManagers> SiteManagers { get; } = new List<SiteManagers>();

}

