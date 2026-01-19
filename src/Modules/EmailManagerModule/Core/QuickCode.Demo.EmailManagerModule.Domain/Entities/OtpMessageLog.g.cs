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

[Table("OTP_MESSAGE_LOGS")]
public partial class OtpMessageLog : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SENDER_ID")]
	public int? SenderId { get; set; }
	
	[Column("OTP_MESSAGE_ID")]
	public int OtpMessageId { get; set; }
	
	[Column("RECIPIENT")]
	[StringLength(250)]
	public string Recipient { get; set; }
	
	[Column("OTP_CODE")]
	[StringLength(250)]
	public string OtpCode { get; set; }
	
	[Column("HASH_CODE")]
	[StringLength(250)]
	public string HashCode { get; set; }
	
	[Column("TEMPLATE_NAME")]
	[StringLength(250)]
	public string TemplateName { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public MessageStatus Status { get; set; }
	
	[Column("EXPIRE_TIME_SECONDS")]
	public int ExpireTimeSeconds { get; set; }
	
	[ForeignKey("TemplateName")]
	[InverseProperty("OtpMessageLogs")]
	public virtual MessageTemplate Template { get; set; } = null!;


	[ForeignKey("SenderId")]
	[InverseProperty("OtpMessageLogs")]
	public virtual Sender Sender { get; set; } = null!;


	[ForeignKey("OtpMessageId")]
	[InverseProperty("OtpMessageLogs")]
	public virtual OtpMessage OtpMessage { get; set; } = null!;

}

