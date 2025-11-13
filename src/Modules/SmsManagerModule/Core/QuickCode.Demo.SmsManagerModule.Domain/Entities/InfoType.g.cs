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

[Table("INFO_TYPES")]
public partial class InfoType : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("TEMPLATE")]
	[StringLength(250)]
	public string Template { get; set; }
	
	[InverseProperty("InfoType")]
	public virtual ICollection<InfoMessage> InfoMessages { get; } = new List<InfoMessage>();

}

