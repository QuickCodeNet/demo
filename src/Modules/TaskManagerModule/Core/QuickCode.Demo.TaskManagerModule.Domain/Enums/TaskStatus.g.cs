using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.TaskManagerModule.Domain.Enums;

public enum TaskStatus{
	[Description("Task is not yet started")]
	Todo,
	[Description("Task is currently being worked on")]
	InProgress,
	[Description("Task is finished")]
	Completed,
	[Description("Task is blocked and cannot be worked on")]
	Blocked
}
