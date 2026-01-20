using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.TaskManagerModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.TaskManagerModule.Domain.Enums;

namespace QuickCode.Demo.TaskManagerModule.Domain.Entities;

[Table("TASKS")]
public partial class Task : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("TITLE")]
	[StringLength(250)]
	public string Title { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(1000)]
	public string Description { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public TaskStatus Status { get; set; }
	
	[Column("PRIORITY")]
	public short Priority { get; set; }
	
	[Column("DUE_DATE")]
	public DateTime DueDate { get; set; }
	
	[Column("CREATED_AT")]
	public DateTime CreatedAt { get; set; }
	
	[Column("UPDATED_AT")]
	public DateTime UpdatedAt { get; set; }
	
	[Column("USER_ID")]
	public int UserId { get; set; }
	
	[InverseProperty("Task")]
	public virtual ICollection<TaskComment> TaskComments { get; } = new List<TaskComment>();


	[ForeignKey("UserId")]
	[InverseProperty("Tasks")]
	public virtual User User { get; set; } = null!;

}

