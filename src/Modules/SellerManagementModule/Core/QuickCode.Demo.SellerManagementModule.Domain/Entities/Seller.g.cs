using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.SellerManagementModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.SellerManagementModule.Domain.Enums;

namespace QuickCode.Demo.SellerManagementModule.Domain.Entities;

[Table("SELLERS")]
public partial class Seller : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("USER_ID")]
	public int UserId { get; set; }
	
	[Column("COMPANY_NAME")]
	[StringLength(250)]
	public string CompanyName { get; set; }
	
	[Column("CONTACT_EMAIL")]
	[StringLength(500)]
	public string ContactEmail { get; set; }
	
	[Column("CONTACT_PHONE")]
	[StringLength(50)]
	public string ContactPhone { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public SellerStatus Status { get; set; }
	
	[Column("SELLER_TIER_ID")]
	public int SellerTierId { get; set; }
	
	[Column("JOINED_DATE")]
	public DateTime JoinedDate { get; set; }
	
	[InverseProperty(nameof(Store.Seller))]
	public virtual ICollection<Store> Stores { get; } = new List<Store>();


	[InverseProperty(nameof(SellerDocument.Seller))]
	public virtual ICollection<SellerDocument> SellerDocuments { get; } = new List<SellerDocument>();


	[InverseProperty(nameof(SellerBankAccount.Seller))]
	public virtual ICollection<SellerBankAccount> SellerBankAccounts { get; } = new List<SellerBankAccount>();


	[InverseProperty(nameof(SellerPerformanceReview.Seller))]
	public virtual ICollection<SellerPerformanceReview> SellerPerformanceReviews { get; } = new List<SellerPerformanceReview>();


	[ForeignKey("SellerTierId")]
	[InverseProperty(nameof(SellerTier.Sellers))]
	public virtual SellerTier SellerTier { get; set; } = null!;

}

