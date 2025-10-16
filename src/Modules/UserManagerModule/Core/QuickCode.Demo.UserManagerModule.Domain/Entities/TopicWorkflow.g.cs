using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.UserManagerModule.Domain;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.UserManagerModule.Domain.Entities;

[Table("TopicWorkflows")]
public partial class TopicWorkflow  : BaseSoftDeletable 
{
	[Key]
	[Column("Id")]
	public int Id { get; set; }
	
	[Column("KafkaEventsTopicName")]
	[StringLength(1000)]
	public string KafkaEventsTopicName { get; set; }
	
	[Column("WorkflowContent")]
	[StringLength(int.MaxValue)]
	public string WorkflowContent { get; set; }
	
	[ForeignKey("KafkaEventsTopicName")]
	[InverseProperty("TopicWorkflows")]
	public virtual KafkaEvent KafkaEvents { get; set; } = null!;

}

