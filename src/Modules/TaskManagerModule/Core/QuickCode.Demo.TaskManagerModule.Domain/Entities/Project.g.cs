using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.TaskManagerModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.TaskManagerModule.Domain.Entities;

[Table("PROJECTS")]
public partial class Project : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(1000)]
	public string Description { get; set; }
	
	[Column("START_DATE")]
	public DateTime StartDate { get; set; }
	
	[Column("END_DATE")]
	public DateTime EndDate { get; set; }
	
	[Column("CREATED_AT")]
	public DateTime CreatedAt { get; set; }
	
	[Column("UPDATED_AT")]
	public DateTime UpdatedAt { get; set; }
	
	[Column("USER_ID")]
	public int UserId { get; set; }
	
	[ForeignKey("UserId")]
	[InverseProperty("Projects")]
	public virtual User User { get; set; } = null!;

}

