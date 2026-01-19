using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.UserManagerModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Domain.Entities;

[Table("PortalPageDefinitions")]
public partial class PortalPageDefinition : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("Key")]
	[StringLength(1000)]
	public string Key { get; set; }
	
	[Column("ModuleName")]
	[StringLength(1000)]
	public string ModuleName { get; set; }
	
	[Column("ModelName")]
	[StringLength(1000)]
	public string ModelName { get; set; }
	
	[Column("PageAction", TypeName = "nvarchar(250)")]
	public PageActionType PageAction { get; set; }
	
	[Column("PagePath")]
	[StringLength(1000)]
	public string PagePath { get; set; }
	
	[InverseProperty("PortalPageDefinition")]
	public virtual ICollection<PortalPageAccessGrant> PortalPageAccessGrants { get; } = new List<PortalPageAccessGrant>();


	[ForeignKey("ModuleName")]
	[InverseProperty("PortalPageDefinitions")]
	public virtual Module Module { get; set; } = null!;


	[ForeignKey("ModelName")]
	[InverseProperty("PortalPageDefinitions")]
	public virtual Model Model { get; set; } = null!;

}

