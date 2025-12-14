using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.OnlineShopModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Domain.Entities;

[Table("CAMPAIGNS")]
public partial class Campaign : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(250)]
	public string Description { get; set; }
	
	[Column("START_DATE")]
	public DateTime StartDate { get; set; }
	
	[Column("END_DATE")]
	public DateTime EndDate { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[Column("BUDGET")]
	public decimal Budget { get; set; }
	
	[Column("TARGET_AUDIENCE", TypeName = "nvarchar(250)")]
	public TargetAudience TargetAudience { get; set; }
	
	[Column("MIN_ORDER_AMOUNT")]
	public decimal MinOrderAmount { get; set; }
	
	[InverseProperty("Campaign")]
	public virtual ICollection<Coupon> Coupons { get; } = new List<Coupon>();

}

