using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.OnlineShopModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.OnlineShopModule.Domain.Entities;

[Table("COUPONS")]
public partial class Coupon : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("COUPON_CODE")]
	[StringLength(250)]
	public string CouponCode { get; set; }
	
	[Column("CAMPAIGN_ID")]
	public int CampaignId { get; set; }
	
	[Column("USER_ID")]
	public int UserId { get; set; }
	
	[Column("DISCOUNT_RATE")]
	public decimal DiscountRate { get; set; }
	
	[Column("DISCOUNT_AMOUNT")]
	public decimal DiscountAmount { get; set; }
	
	[Column("MAX_USAGE")]
	public int MaxUsage { get; set; }
	
	[Column("USED_COUNT")]
	public int UsedCount { get; set; }
	
	[Column("START_DATE")]
	public DateTime StartDate { get; set; }
	
	[Column("END_DATE")]
	public DateTime EndDate { get; set; }
	
	[Column("IS_SINGLE_USE")]
	public bool IsSingleUse { get; set; }
	
	[InverseProperty("Coupon")]
	public virtual ICollection<UserCoupon> UserCoupons { get; } = new List<UserCoupon>();


	[ForeignKey("CampaignId")]
	[InverseProperty("Coupons")]
	public virtual Campaign Campaign { get; set; } = null!;


	[ForeignKey("UserId")]
	[InverseProperty("Coupons")]
	public virtual User User { get; set; } = null!;

}

