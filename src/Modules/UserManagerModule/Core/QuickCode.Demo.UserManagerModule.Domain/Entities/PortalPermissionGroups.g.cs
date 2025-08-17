using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.UserManagerModule.Domain.Entities;

[PrimaryKey("PortalPermissionName", "PermissionGroupName", "PortalPermissionTypeId")]
[Table("PortalPermissionGroups")]
public partial class PortalPermissionGroups  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("PortalPermissionName")]
	[StringLength(1000)]
	public string PortalPermissionName { get; set; }
	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("PermissionGroupName")]
	[StringLength(1000)]
	public string PermissionGroupName { get; set; }
	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("PortalPermissionTypeId")]
	public int PortalPermissionTypeId { get; set; }
	
	[Column("IsActive")]
	public bool IsActive { get; set; }
	
	[ForeignKey("PortalPermissionTypeId")]
	[InverseProperty("PortalPermissionGroups")]
	public virtual PortalPermissionTypes PortalPermissionType { get; set; } = null!;


	[ForeignKey("PortalPermissionName")]
	[InverseProperty("PortalPermissionGroups")]
	public virtual PortalPermissions PortalPermission { get; set; } = null!;


	[ForeignKey("PermissionGroupName")]
	[InverseProperty("PortalPermissionGroups")]
	public virtual PermissionGroups PermissionGroup { get; set; } = null!;

}

