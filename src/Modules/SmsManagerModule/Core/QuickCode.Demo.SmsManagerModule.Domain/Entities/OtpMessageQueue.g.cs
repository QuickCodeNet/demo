using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.SmsManagerModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Domain.Entities;

[Table("OTP_MESSAGE_QUEUES")]
public partial class OtpMessageQueue : BaseSoftDeletable, IAuditableEntity 
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
	
	[Column("PRIORITY")]
	public int Priority { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public MessageStatus Status { get; set; }
	
	[Column("PROCESS_DATE")]
	public DateTime? ProcessDate { get; set; }
	
	[ForeignKey("OtpMessageId")]
	[InverseProperty("OtpMessageQueues")]
	public virtual OtpMessage OtpMessage { get; set; } = null!;


	[ForeignKey("SenderId")]
	[InverseProperty("OtpMessageQueues")]
	public virtual Sender Sender { get; set; } = null!;

}

