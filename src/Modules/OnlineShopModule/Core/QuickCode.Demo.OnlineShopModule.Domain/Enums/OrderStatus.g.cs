using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.OnlineShopModule.Domain.Enums;

public enum OrderStatus{
	[Description("Bekliyor")]
	Pending,
	[Description("Ödendi")]
	Paid,
	[Description("Kargolandı")]
	Shipped,
	[Description("Teslim Edildi")]
	Delivered,
	[Description("İptal Edildi")]
	Cancelled
}
