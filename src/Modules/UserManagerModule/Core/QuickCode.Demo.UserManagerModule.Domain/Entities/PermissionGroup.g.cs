using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.UserManagerModule.Domain;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.UserManagerModule.Domain.Entities;

[Table("PermissionGroups")]
public partial class PermissionGroup  
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
	public virtual ICollection<AspNetUser> AspNetUsers { get; } = new List<AspNetUser>();


	[InverseProperty("PermissionGroup")]
	public virtual ICollection<PortalPermissionGroup> PortalPermissionGroups { get; } = new List<PortalPermissionGroup>();


	[InverseProperty("PermissionGroup")]
	public virtual ICollection<ApiPermissionGroup> ApiPermissionGroups { get; } = new List<ApiPermissionGroup>();

}

