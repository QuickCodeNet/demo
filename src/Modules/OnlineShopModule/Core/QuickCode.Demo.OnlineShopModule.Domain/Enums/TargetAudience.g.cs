using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.OnlineShopModule.Domain.Enums;

public enum TargetAudience{
	[Description("Tüm Kullanıcılar")]
	All,
	[Description("Yeni Kullanıcılar")]
	NewUsers,
	[Description("Firma Kullanıcıları")]
	CompanyUsers
}
