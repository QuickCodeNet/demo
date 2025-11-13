using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.SmsManagerModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.SmsManagerModule.Domain.Entities;

[Table("SMS_SENDERS")]
public partial class SmsSender : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("GSM_NUMBER")]
	[StringLength(250)]
	public string GsmNumber { get; set; }
	
	[Column("PROVIDER_NAME")]
	[StringLength(250)]
	public string ProviderName { get; set; }
	
	[InverseProperty("SmsSender")]
	public virtual ICollection<InfoMessage> InfoMessages { get; } = new List<InfoMessage>();


	[InverseProperty("SmsSender")]
	public virtual ICollection<OtpMessage> OtpMessages { get; } = new List<OtpMessage>();


	[InverseProperty("SmsSender")]
	public virtual ICollection<CampaignMessage> CampaignMessages { get; } = new List<CampaignMessage>();

}

