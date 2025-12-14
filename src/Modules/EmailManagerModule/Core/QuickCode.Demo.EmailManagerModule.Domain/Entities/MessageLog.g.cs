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

[Table("MESSAGE_LOGS")]
public partial class MessageLog : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("MESSAGE_ID")]
	public int MessageId { get; set; }
	
	[Column("CAMPAIGN_ID")]
	public int CampaignId { get; set; }
	
	[Column("SENDER_ID")]
	public int? SenderId { get; set; }
	
	[Column("ATTEMPT_DATE")]
	public DateTime AttemptDate { get; set; }
	
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
	
	[Column("SENT_DATE")]
	public DateTime? SentDate { get; set; }
	
	[ForeignKey("TemplateName")]
	[InverseProperty("MessageLogs")]
	public virtual MessageTemplate Template { get; set; } = null!;


	[ForeignKey("SenderId")]
	[InverseProperty("MessageLogs")]
	public virtual Sender Sender { get; set; } = null!;


	[ForeignKey("MessageId")]
	[InverseProperty("MessageLogs")]
	public virtual Message Message { get; set; } = null!;

}

