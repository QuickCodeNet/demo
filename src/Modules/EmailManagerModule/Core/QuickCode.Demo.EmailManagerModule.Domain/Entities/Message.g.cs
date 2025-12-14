using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.EmailManagerModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.EmailManagerModule.Domain.Enums;

namespace QuickCode.Demo.EmailManagerModule.Domain.Entities;

[Table("MESSAGES")]
public partial class Message : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("CAMPAIGN_ID")]
	public int CampaignId { get; set; }
	
	[Column("RECIPIENT")]
	[StringLength(250)]
	public string Recipient { get; set; }
	
	[Column("SUBJECT")]
	[StringLength(250)]
	public string Subject { get; set; }
	
	[Column("BODY")]
	[StringLength(250)]
	public string Body { get; set; }
	
	[Column("TEMPLATE_NAME")]
	[StringLength(250)]
	public string TemplateName { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public MessageStatus Status { get; set; }
	
	[Column("ATTEMPT_COUNT")]
	public int AttemptCount { get; set; }
	
	[Column("LAST_ATTEMPT_DATE")]
	public DateTime? LastAttemptDate { get; set; }
	
	[Column("SENT_DATE")]
	public DateTime? SentDate { get; set; }
	
	[InverseProperty("Message")]
	public virtual ICollection<MessageQueue> MessageQueues { get; } = new List<MessageQueue>();


	[InverseProperty("Message")]
	public virtual ICollection<MessageLog> MessageLogs { get; } = new List<MessageLog>();


	[ForeignKey("CampaignId")]
	[InverseProperty("Messages")]
	public virtual Campaign Campaign { get; set; } = null!;


	[ForeignKey("TemplateName")]
	[InverseProperty("Messages")]
	public virtual MessageTemplate Template { get; set; } = null!;

}

