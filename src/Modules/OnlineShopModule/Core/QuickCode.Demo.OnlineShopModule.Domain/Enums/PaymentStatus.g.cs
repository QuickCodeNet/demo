using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.OnlineShopModule.Domain.Enums;

public enum PaymentStatus{
	[Description("Bekliyor")]
	Pending,
	[Description("Tamamlandı")]
	Completed,
	[Description("Başarısız")]
	Failed,
	[Description("İade Edildi")]
	Refunded
}
