using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.UserManagerModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.UserManagerModule.Domain.Entities;

[Table("AspNetRoles")]
public partial class AspNetRole : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("Id")]
	[StringLength(450)]
	public string Id { get; set; }
	
	[Column("Name")]
	[StringLength(256)]
	public string Name { get; set; }
	
	[Column("NormalizedName")]
	[StringLength(256)]
	public string? NormalizedName { get; set; }
	
	[Column("ConcurrencyStamp")]
	[StringLength(int.MaxValue)]
	public string? ConcurrencyStamp { get; set; }
	
	[InverseProperty("Role")]
	public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; } = new List<AspNetUserRole>();


	[InverseProperty("Role")]
	public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; } = new List<AspNetRoleClaim>();

}

