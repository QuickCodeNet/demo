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

[Table("TASK_COMMENTS")]
public partial class TaskComment : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("TASK_ID")]
	public int TaskId { get; set; }
	
	[Column("USER_ID")]
	public int UserId { get; set; }
	
	[Column("COMMENT")]
	[StringLength(int.MaxValue)]
	public string Comment { get; set; }
	
	[Column("CREATED_AT")]
	public DateTime CreatedAt { get; set; }
	
	[ForeignKey("TaskId")]
	[InverseProperty("TaskComments")]
	public virtual Task Task { get; set; } = null!;


	[ForeignKey("UserId")]
	[InverseProperty("TaskComments")]
	public virtual User User { get; set; } = null!;

}

