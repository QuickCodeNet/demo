using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.UserManagerModule.Domain.Entities;

[Table("PermissionGroups")]
public partial class PermissionGroups  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("Name")]
	[StringLength(1000)]
	public string Name { get; set; }
	
	[Column("Description")]
	[StringLength(1000)]
	public string Description { get; set; }
	
	[InverseProperty("PermissionGroup")]
	public virtual ICollection<AspNetUsers> AspNetUsers { get; } = new List<AspNetUsers>();


	[InverseProperty("PermissionGroup")]
	public virtual ICollection<PortalPermissionGroups> PortalPermissionGroups { get; } = new List<PortalPermissionGroups>();


	[InverseProperty("PermissionGroup")]
	public virtual ICollection<ApiPermissionGroups> ApiPermissionGroups { get; } = new List<ApiPermissionGroups>();

}

