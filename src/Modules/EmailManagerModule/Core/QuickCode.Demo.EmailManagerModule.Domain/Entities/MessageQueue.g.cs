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

[Table("MESSAGE_QUEUES")]
public partial class MessageQueue : BaseSoftDeletable, IAuditableEntity 
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
	
	[Column("RECIPIENT")]
	[StringLength(250)]
	public string Recipient { get; set; }
	
	[Column("SUBJECT")]
	[StringLength(250)]
	public string Subject { get; set; }
	
	[Column("BODY")]
	[StringLength(250)]
	public string Body { get; set; }
	
	[Column("PRIORITY")]
	public int Priority { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public MessageStatus Status { get; set; }
	
	[Column("PROCESS_DATE")]
	public DateTime? ProcessDate { get; set; }
	
	[ForeignKey("CampaignId")]
	[InverseProperty("MessageQueues")]
	public virtual Campaign Campaign { get; set; } = null!;


	[ForeignKey("MessageId")]
	[InverseProperty("MessageQueues")]
	public virtual Message Message { get; set; } = null!;


	[ForeignKey("SenderId")]
	[InverseProperty("MessageQueues")]
	public virtual Sender Sender { get; set; } = null!;

}

