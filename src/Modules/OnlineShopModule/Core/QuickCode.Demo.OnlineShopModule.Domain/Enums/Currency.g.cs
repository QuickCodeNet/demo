using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.OnlineShopModule.Domain.Enums;

public enum Currency{
	[Description("Türk Lirası")]
	Try,
	[Description("Amerikan Doları")]
	Usd,
	[Description("Euro")]
	Eur
}
