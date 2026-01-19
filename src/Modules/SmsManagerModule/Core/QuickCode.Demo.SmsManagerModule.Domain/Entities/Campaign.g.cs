using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.SmsManagerModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.SmsManagerModule.Domain.Entities;

[Table("CAMPAIGNS")]
public partial class Campaign : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(250)]
	public string? Description { get; set; }
	
	[Column("START_DATE")]
	public DateTime StartDate { get; set; }
	
	[Column("END_DATE")]
	public DateTime EndDate { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[Column("PRIORITY")]
	public int Priority { get; set; }
	
	[Column("TEMPLATE_NAME")]
	[StringLength(250)]
	public string TemplateName { get; set; }
	
	[InverseProperty("Campaign")]
	public virtual ICollection<Message> Messages { get; } = new List<Message>();


	[InverseProperty("Campaign")]
	public virtual ICollection<MessageQueue> MessageQueues { get; } = new List<MessageQueue>();


	[ForeignKey("TemplateName")]
	[InverseProperty("Campaigns")]
	public virtual MessageTemplate Template { get; set; } = null!;

}

