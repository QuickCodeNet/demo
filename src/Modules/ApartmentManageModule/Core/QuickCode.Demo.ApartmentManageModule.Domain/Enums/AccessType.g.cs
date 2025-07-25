using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Enums;

public enum AccessType{
	[Description("Ev Sahibi")]
	Owner,
	[Description("Kiracı")]
	Tenant,
	[Description("Yönetici")]
	Manager,
	[Description("Yönetici")]
	Admin,
	[Description("Görüntüleyici")]
	Viewer
}
