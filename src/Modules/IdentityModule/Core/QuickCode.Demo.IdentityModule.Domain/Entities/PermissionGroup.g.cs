using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.IdentityModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.IdentityModule.Domain.Entities;

[Table("PermissionGroups")]
public partial class PermissionGroup : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("Name")]
	[StringLength(1000)]
	public string Name { get; set; }
	
	[Column("Description")]
	[StringLength(1000)]
	public string Description { get; set; }
	
	[InverseProperty(nameof(ApiMethodAccessGrant.PermissionGroup))]
	public virtual ICollection<ApiMethodAccessGrant> ApiMethodAccessGrants { get; } = new List<ApiMethodAccessGrant>();


	[InverseProperty(nameof(PortalPageAccessGrant.PermissionGroup))]
	public virtual ICollection<PortalPageAccessGrant> PortalPageAccessGrants { get; } = new List<PortalPageAccessGrant>();


	[InverseProperty(nameof(AspNetUser.PermissionGroup))]
	public virtual ICollection<AspNetUser> AspNetUsers { get; } = new List<AspNetUser>();

}

