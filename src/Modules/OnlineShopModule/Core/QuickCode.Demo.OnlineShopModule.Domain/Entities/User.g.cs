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

[Table("USERS")]
public partial class User : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("EMAIL")]
	[StringLength(250)]
	public string Email { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("CREATED_AT")]
	public DateTime CreatedAt { get; set; }
	
	[Column("IS_NEW")]
	public bool IsNew { get; set; }
	
	[Column("COMPANY_ID")]
	public int CompanyId { get; set; }
	
	[InverseProperty("User")]
	public virtual ICollection<Coupon> Coupons { get; } = new List<Coupon>();


	[InverseProperty("User")]
	public virtual ICollection<UserCoupon> UserCoupons { get; } = new List<UserCoupon>();


	[InverseProperty("User")]
	public virtual ICollection<ProductReview> ProductReviews { get; } = new List<ProductReview>();


	[InverseProperty("User")]
	public virtual ICollection<Cart> Carts { get; } = new List<Cart>();


	[InverseProperty("User")]
	public virtual ICollection<Order> Orders { get; } = new List<Order>();


	[ForeignKey("CompanyId")]
	[InverseProperty("Users")]
	public virtual Company Company { get; set; } = null!;

}

