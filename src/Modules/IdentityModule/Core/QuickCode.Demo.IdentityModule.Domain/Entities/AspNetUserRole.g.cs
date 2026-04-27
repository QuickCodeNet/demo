using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.IdentityModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.IdentityModule.Domain.Entities;

[Table("AspNetUserRoles")]
public partial class AspNetUserRole : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("UserId")]
	[StringLength(450)]
	public string UserId { get; set; }
	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("RoleId")]
	[StringLength(450)]
	public string RoleId { get; set; }
	
	[ForeignKey("RoleId")]
	[InverseProperty(nameof(AspNetRole.AspNetUserRoles))]
	public virtual AspNetRole AspNetRole { get; set; } = null!;


	[ForeignKey("UserId")]
	[InverseProperty(nameof(AspNetUser.AspNetUserRoles))]
	public virtual AspNetUser AspNetUser { get; set; } = null!;

}

