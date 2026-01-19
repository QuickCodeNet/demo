using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.EmailManagerModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.EmailManagerModule.Domain.Entities;

[Table("SENDERS")]
public partial class Sender : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("EMAIL_ADDRESS")]
	[StringLength(250)]
	public string EmailAddress { get; set; }
	
	[Column("API_URL")]
	[StringLength(250)]
	public string ApiUrl { get; set; }
	
	[Column("USERNAME")]
	[StringLength(250)]
	public string Username { get; set; }
	
	[Column("PASSWORD")]
	[StringLength(250)]
	public string Password { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[Column("PRIORITY")]
	public int Priority { get; set; }
	
	[Column("DAILY_LIMIT")]
	public int DailyLimit { get; set; }
	
	[InverseProperty("Sender")]
	public virtual ICollection<MessageLog> MessageLogs { get; } = new List<MessageLog>();


	[InverseProperty("Sender")]
	public virtual ICollection<OtpMessageLog> OtpMessageLogs { get; } = new List<OtpMessageLog>();


	[InverseProperty("Sender")]
	public virtual ICollection<MessageQueue> MessageQueues { get; } = new List<MessageQueue>();


	[InverseProperty("Sender")]
	public virtual ICollection<OtpMessageQueue> OtpMessageQueues { get; } = new List<OtpMessageQueue>();

}

