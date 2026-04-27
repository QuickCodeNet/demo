using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.IdentityModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.IdentityModule.Domain.Entities;

[Table("AspNetRoleClaims")]
public partial class AspNetRoleClaim : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("Id")]
	public int Id { get; set; }
	
	[Column("RoleId")]
	[StringLength(450)]
	public string RoleId { get; set; }
	
	[Column("ClaimType")]
	[StringLength(int.MaxValue)]
	public string? ClaimType { get; set; }
	
	[Column("ClaimValue")]
	[StringLength(int.MaxValue)]
	public string? ClaimValue { get; set; }
	
	[ForeignKey("RoleId")]
	[InverseProperty(nameof(AspNetRole.AspNetRoleClaims))]
	public virtual AspNetRole AspNetRole { get; set; } = null!;

}

