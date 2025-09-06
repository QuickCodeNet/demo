using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.UserManagerModule.Domain.Enums;

public enum HttpMethodType{
	[Description("Fetch")]
	Get,
	[Description("Create")]
	Post,
	[Description("Update")]
	Put,
	[Description("Remove")]
	Delete,
	[Description("Modify")]
	Patch
}
