using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.UserManagerModule.Domain.Entities;

[PrimaryKey("PermissionGroupName", "ApiMethodDefinitionKey")]
[Table("ApiPermissionGroups")]
public partial class ApiPermissionGroups  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("PermissionGroupName")]
	[StringLength(1000)]
	public string PermissionGroupName { get; set; }
	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("ApiMethodDefinitionKey")]
	[StringLength(1000)]
	public string ApiMethodDefinitionKey { get; set; }
	
	[Column("IsActive")]
	public bool IsActive { get; set; }
	
	[ForeignKey("ApiMethodDefinitionKey")]
	[InverseProperty("ApiPermissionGroups")]
	public virtual ApiMethodDefinitions ApiMethodDefinition { get; set; } = null!;


	[ForeignKey("PermissionGroupName")]
	[InverseProperty("ApiPermissionGroups")]
	public virtual PermissionGroups PermissionGroup { get; set; } = null!;

}

