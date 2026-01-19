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

[Table("OTP_MESSAGES")]
public partial class OtpMessage : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
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
	
	[Column("ATTEMPT_COUNT")]
	public int AttemptCount { get; set; }
	
	[InverseProperty("OtpMessage")]
	public virtual ICollection<OtpMessageQueue> OtpMessageQueues { get; } = new List<OtpMessageQueue>();


	[InverseProperty("OtpMessage")]
	public virtual ICollection<OtpMessageLog> OtpMessageLogs { get; } = new List<OtpMessageLog>();


	[ForeignKey("TemplateName")]
	[InverseProperty("OtpMessages")]
	public virtual MessageTemplate Template { get; set; } = null!;

}

