using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.OnlineShopModule.Domain.Enums;

public enum CompanySector{
	[Description("Teknoloji")]
	Technology,
	[Description("Perakende")]
	Retail,
	[Description("Üretim")]
	Manufacturing,
	[Description("Hizmet")]
	Services
}
