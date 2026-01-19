using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.OnlineShopModule.Domain.Enums;

public enum CargoCompany{
	[Description("Yurtiçi Kargo")]
	Yurtici,
	[Description("Aras Kargo")]
	Aras,
	[Description("MNG Kargo")]
	Mng,
	[Description("Sürat Kargo")]
	Surat,
	[Description("PTT Kargo")]
	Ptt
}
