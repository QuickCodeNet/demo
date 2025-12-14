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

[Table("BLACK_LISTS")]
public partial class BlackList : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("RECIPIENT")]
	[StringLength(250)]
	public string Recipient { get; set; }
	
	[Column("REASON")]
	[StringLength(250)]
	public string Reason { get; set; }
	
	[Column("REASON_TYPE", TypeName = "nvarchar(250)")]
	public BlacklistReasonType ReasonType { get; set; }
	}

