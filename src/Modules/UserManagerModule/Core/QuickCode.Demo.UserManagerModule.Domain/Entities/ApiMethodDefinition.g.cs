﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.UserManagerModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Domain.Entities;

[Table("ApiMethodDefinitions")]
public partial class ApiMethodDefinition  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("Key")]
	[StringLength(1000)]
	public string Key { get; set; }
	
	[Column("HttpMethod", TypeName = "nvarchar(200)")]
	public HttpMethodType HttpMethod { get; set; }
	
	[Column("ControllerName")]
	[StringLength(1000)]
	public string ControllerName { get; set; }
	
	[Column("UrlPath")]
	[StringLength(250)]
	public string UrlPath { get; set; }
	
	[InverseProperty("ApiMethodDefinition")]
	public virtual ICollection<KafkaEvent> KafkaEvents { get; } = new List<KafkaEvent>();


	[InverseProperty("ApiMethodDefinition")]
	public virtual ICollection<ApiPermissionGroup> ApiPermissionGroups { get; } = new List<ApiPermissionGroup>();

}

