using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.IdentityModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.IdentityModule.Domain.Entities;

[Table("TopicWorkflows")]
public partial class TopicWorkflow : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("Id")]
	public int Id { get; set; }
	
	[Column("KafkaEventsTopicName")]
	[StringLength(1000)]
	public string KafkaEventsTopicName { get; set; }
	
	[Column("WorkflowContent")]
	[StringLength(int.MaxValue)]
	public string WorkflowContent { get; set; }
	
	[ForeignKey("KafkaEventsTopicName")]
	[InverseProperty(nameof(KafkaEvent.TopicWorkflows))]
	public virtual KafkaEvent KafkaEvent { get; set; } = null!;

}

