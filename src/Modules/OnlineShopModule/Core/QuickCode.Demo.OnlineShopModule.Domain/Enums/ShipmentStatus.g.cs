using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.OnlineShopModule.Domain.Enums;

public enum ShipmentStatus{
	[Description("Bekliyor")]
	Pending,
	[Description("Kargolandı")]
	Shipped,
	[Description("Teslim Edildi")]
	Delivered,
	[Description("İptal Edildi")]
	Cancelled
}
