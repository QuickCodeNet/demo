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

[Table("USERS")]
public partial class User : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("USERNAME")]
	[StringLength(250)]
	public string Username { get; set; }
	
	[Column("EMAIL")]
	[StringLength(500)]
	public string Email { get; set; }
	
	[Column("PASSWORD")]
	[StringLength(250)]
	public string Password { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[InverseProperty("User")]
	public virtual ICollection<Task> Tasks { get; } = new List<Task>();


	[InverseProperty("User")]
	public virtual ICollection<Project> Projects { get; } = new List<Project>();


	[InverseProperty("User")]
	public virtual ICollection<TaskComment> TaskComments { get; } = new List<TaskComment>();

}

