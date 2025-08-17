using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.UserManagerModule.Domain.Entities;

[Table("KafkaEvents")]
public partial class KafkaEvents  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("TopicName")]
	[StringLength(1000)]
	public string TopicName { get; set; }
	
	[Column("ApiMethodDefinitionKey")]
	[StringLength(1000)]
	public string ApiMethodDefinitionKey { get; set; }
	
	[Column("IsActive")]
	public bool IsActive { get; set; }
	
	[InverseProperty("KafkaEvents")]
	public virtual ICollection<TopicWorkflows> TopicWorkflows { get; } = new List<TopicWorkflows>();


	[ForeignKey("ApiMethodDefinitionKey")]
	[InverseProperty("KafkaEvents")]
	public virtual ApiMethodDefinitions ApiMethodDefinition { get; set; } = null!;

}

