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

[PrimaryKey("UserId", "CouponId")]
[Table("USER_COUPONS")]
public partial class UserCoupon : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("USER_ID")]
	public int UserId { get; set; }
	
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("COUPON_ID")]
	public int CouponId { get; set; }
	
	[Column("IS_USED")]
	public bool IsUsed { get; set; }
	
	[Column("USED_AT")]
	public DateTime UsedAt { get; set; }
	
	[ForeignKey("UserId")]
	[InverseProperty("UserCoupons")]
	public virtual User User { get; set; } = null!;


	[ForeignKey("CouponId")]
	[InverseProperty("UserCoupons")]
	public virtual Coupon Coupon { get; set; } = null!;

}

