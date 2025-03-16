using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.EmailManagerModule.Domain.Entities;

[Table("CAMPAIGN_MESSAGES")]
public partial class CampaignMessages  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("EMAIL_SENDER_ID")]
	public int EmailSenderId { get; set; }
	
	[Column("CAMPAIGN_TYPE_ID")]
	public int CampaignTypeId { get; set; }
	
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
	[InverseProperty("CampaignMessages")]
	public virtual EmailSenders EmailSender { get; set; } = null!;


	[ForeignKey("CampaignTypeId")]
	[InverseProperty("CampaignMessages")]
	public virtual CampaignTypes CampaignType { get; set; } = null!;

}

