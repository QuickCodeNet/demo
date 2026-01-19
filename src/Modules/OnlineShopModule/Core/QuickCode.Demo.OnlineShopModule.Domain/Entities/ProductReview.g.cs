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

[Table("PRODUCT_REVIEWS")]
public partial class ProductReview : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("PRODUCT_ID")]
	public int ProductId { get; set; }
	
	[Column("USER_ID")]
	public int UserId { get; set; }
	
	[Column("RATING")]
	public int Rating { get; set; }
	
	[Column("COMMENT")]
	[StringLength(250)]
	public string Comment { get; set; }
	
	[Column("CREATED_AT")]
	public DateTime CreatedAt { get; set; }
	
	[ForeignKey("ProductId")]
	[InverseProperty("ProductReviews")]
	public virtual Product Product { get; set; } = null!;


	[ForeignKey("UserId")]
	[InverseProperty("ProductReviews")]
	public virtual User User { get; set; } = null!;

}

