using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.EmailManagerModule.Domain;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.EmailManagerModule.Domain.Entities;

[Table("INFO_TYPES")]
public partial class InfoType  : BaseSoftDeletable 
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
	
	[InverseProperty("InfoType")]
	public virtual ICollection<InfoMessage> InfoMessages { get; } = new List<InfoMessage>();

}

