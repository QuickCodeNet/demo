using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.SellerManagementModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.SellerManagementModule.Domain.Entities;

[Table("SELLER_PERFORMANCE_REVIEWS")]
public partial class SellerPerformanceReview : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SELLER_ID")]
	public int SellerId { get; set; }
	
	[Column("REVIEWER_ID")]
	public int ReviewerId { get; set; }
	
	[Column("RATING")]
	public short Rating { get; set; }
	
	[Column("COMMENT")]
	[StringLength(1000)]
	public string Comment { get; set; }
	
	[Column("REVIEW_DATE")]
	public DateTime ReviewDate { get; set; }
	
	[ForeignKey("SellerId")]
	[InverseProperty(nameof(Seller.SellerPerformanceReviews))]
	public virtual Seller Seller { get; set; } = null!;

}

