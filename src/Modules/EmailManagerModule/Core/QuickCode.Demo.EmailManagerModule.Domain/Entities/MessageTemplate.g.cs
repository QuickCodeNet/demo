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

[Table("MESSAGE_TEMPLATES")]
public partial class MessageTemplate : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("CONTENT")]
	[StringLength(250)]
	public string Content { get; set; }
	
	[Column("TYPE", TypeName = "nvarchar(250)")]
	public TemplateTypes Type { get; set; }
	
	[InverseProperty("Template")]
	public virtual ICollection<Campaign> Campaigns { get; } = new List<Campaign>();


	[InverseProperty("Template")]
	public virtual ICollection<Message> Messages { get; } = new List<Message>();


	[InverseProperty("Template")]
	public virtual ICollection<OtpMessage> OtpMessages { get; } = new List<OtpMessage>();


	[InverseProperty("Template")]
	public virtual ICollection<OtpMessageLog> OtpMessageLogs { get; } = new List<OtpMessageLog>();


	[InverseProperty("Template")]
	public virtual ICollection<MessageLog> MessageLogs { get; } = new List<MessageLog>();

}

