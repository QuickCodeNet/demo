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

[Table("SELLER_DOCUMENTS")]
public partial class SellerDocument : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SELLER_ID")]
	public int SellerId { get; set; }
	
	[Column("DOCUMENT_TYPE", TypeName = "nvarchar(250)")]
	public DocumentType DocumentType { get; set; }
	
	[Column("FILE_URL")]
	[StringLength(1000)]
	public string FileUrl { get; set; }
	
	[Column("STATUS", TypeName = "nvarchar(250)")]
	public VerificationStatus Status { get; set; }
	
	[Column("UPLOADED_DATE")]
	public DateTime UploadedDate { get; set; }
	
	[Column("REVIEWED_DATE")]
	public DateTime ReviewedDate { get; set; }
	
	[Column("REJECTION_REASON")]
	[StringLength(1000)]
	public string RejectionReason { get; set; }
	
	[ForeignKey("SellerId")]
	[InverseProperty(nameof(Seller.SellerDocuments))]
	public virtual Seller Seller { get; set; } = null!;

}

