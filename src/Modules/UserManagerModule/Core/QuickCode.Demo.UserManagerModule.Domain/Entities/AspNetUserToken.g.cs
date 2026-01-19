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

[Table("AspNetUserTokens")]
public partial class AspNetUserToken : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("UserId")]
	[StringLength(450)]
	public string UserId { get; set; }
	
	[Column("LoginProvider")]
	[StringLength(450)]
	public string LoginProvider { get; set; }
	
	[Column("Name")]
	[StringLength(450)]
	public string Name { get; set; }
	
	[Column("Value")]
	[StringLength(int.MaxValue)]
	public string? Value { get; set; }
	
	[ForeignKey("UserId")]
	[InverseProperty("AspNetUserTokens")]
	public virtual AspNetUser User { get; set; } = null!;

}

