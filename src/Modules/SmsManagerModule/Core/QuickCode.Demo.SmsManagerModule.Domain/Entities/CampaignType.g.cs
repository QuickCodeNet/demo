using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.SmsManagerModule.Domain;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.SmsManagerModule.Domain.Entities;

[Table("CAMPAIGN_TYPES")]
public partial class CampaignType  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("TEMPLATE")]
	[StringLength(250)]
	public string Template { get; set; }
	
	[InverseProperty("CampaignType")]
	public virtual ICollection<CampaignMessage> CampaignMessages { get; } = new List<CampaignMessage>();

}

