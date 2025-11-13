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

[Table("INFO_MESSAGES")]
public partial class InfoMessage : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("EMAIL_SENDER_ID")]
	public int EmailSenderId { get; set; }
	
	[Column("INFO_TYPE_ID")]
	public int InfoTypeId { get; set; }
	
	[Column("GSM_NUMBER")]
	[StringLength(250)]
	public string GsmNumber { get; set; }
	
	[Column("MESSAGE")]
	[StringLength(250)]
	public string Message { get; set; }
	
	[Column("MESSAGE_DATE")]
	public DateTime? MessageDate { get; set; }
	
	[Column("MESSAGE_SID")]
	[StringLength(250)]
	public string MessageSid { get; set; }
	
	[Column("DAILY_COUNTER")]
	public int DailyCounter { get; set; }
	
	[ForeignKey("EmailSenderId")]
	[InverseProperty("InfoMessages")]
	public virtual EmailSender EmailSender { get; set; } = null!;


	[ForeignKey("InfoTypeId")]
	[InverseProperty("InfoMessages")]
	public virtual InfoType InfoType { get; set; } = null!;

}

