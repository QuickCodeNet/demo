using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.UserManagerModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Domain.Entities;

[Table("ApiMethodDefinitions")]
public partial class ApiMethodDefinition : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("Key")]
	[StringLength(1000)]
	public string Key { get; set; }
	
	[Column("ModuleName")]
	[StringLength(1000)]
	public string ModuleName { get; set; }
	
	[Column("ModelName")]
	[StringLength(1000)]
	public string ModelName { get; set; }
	
	[Column("HttpMethod", TypeName = "nvarchar(250)")]
	public HttpMethodType HttpMethod { get; set; }
	
	[Column("ControllerName")]
	[StringLength(1000)]
	public string ControllerName { get; set; }
	
	[Column("MethodName")]
	[StringLength(1000)]
	public string MethodName { get; set; }
	
	[Column("UrlPath")]
	[StringLength(1000)]
	public string UrlPath { get; set; }
	
	[InverseProperty("ApiMethodDefinition")]
	public virtual ICollection<ApiMethodAccessGrant> ApiMethodAccessGrants { get; } = new List<ApiMethodAccessGrant>();


	[InverseProperty("ApiMethodDefinition")]
	public virtual ICollection<KafkaEvent> KafkaEvents { get; } = new List<KafkaEvent>();


	[ForeignKey("ModuleName")]
	[InverseProperty("ApiMethodDefinitions")]
	public virtual Module Module { get; set; } = null!;


	[ForeignKey("ModelName")]
	[InverseProperty("ApiMethodDefinitions")]
	public virtual Model Model { get; set; } = null!;

}

