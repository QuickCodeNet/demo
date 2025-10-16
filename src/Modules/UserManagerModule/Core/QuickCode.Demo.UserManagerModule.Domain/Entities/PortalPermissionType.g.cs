using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.UserManagerModule.Domain;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.UserManagerModule.Domain.Entities;

[Table("PortalPermissionTypes")]
public partial class PortalPermissionType  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("Id")]
	public int Id { get; set; }
	
	[Column("Name")]
	[StringLength(100)]
	public string Name { get; set; }
	
	[Column("Description")]
	[StringLength(1000)]
	public string Description { get; set; }
	
	[InverseProperty("PortalPermissionType")]
	public virtual ICollection<PortalPermissionGroup> PortalPermissionGroups { get; } = new List<PortalPermissionGroup>();

}

