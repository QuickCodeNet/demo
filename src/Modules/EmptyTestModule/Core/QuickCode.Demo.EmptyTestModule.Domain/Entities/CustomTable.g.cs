using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.EmptyTestModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.EmptyTestModule.Domain.Entities;

[Table("CUSTOM_TABLES")]
public partial class CustomTable : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("SURNAME")]
	[StringLength(250)]
	public string Surname { get; set; }
	
	[Column("JSON_DATA")]
	[StringLength(int.MaxValue)]
	public string JsonData { get; set; }
	}

