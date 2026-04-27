using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuickCode.Demo.SellerManagementModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.SellerManagementModule.Domain.Entities;

[Table("SELLER_BANK_ACCOUNTS")]
public partial class SellerBankAccount : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("SELLER_ID")]
	public int SellerId { get; set; }
	
	[Column("BANK_NAME")]
	[StringLength(250)]
	public string BankName { get; set; }
	
	[Column("ACCOUNT_HOLDER_NAME")]
	[StringLength(250)]
	public string AccountHolderName { get; set; }
	
	[Column("IBAN")]
	[StringLength(50)]
	public string Iban { get; set; }
	
	[Column("IS_DEFAULT")]
	public bool IsDefault { get; set; }
	
	[Column("IS_VERIFIED")]
	public bool IsVerified { get; set; }
	
	[ForeignKey("SellerId")]
	[InverseProperty(nameof(Seller.SellerBankAccounts))]
	public virtual Seller Seller { get; set; } = null!;

}

