using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.IdentityModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.IdentityModule.Domain.Entities;

[Table("TableComboboxSettings")]
public partial class TableComboboxSetting : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("TableName")]
	[StringLength(250)]
	public string TableName { get; set; }
	
	[Column("IdColumn")]
	[StringLength(250)]
	public string IdColumn { get; set; }
	
	[Column("TextColumns")]
	[StringLength(int.MaxValue)]
	public string TextColumns { get; set; }
	
	[Column("StringFormat")]
	[StringLength(int.MaxValue)]
	public string StringFormat { get; set; }
	}

