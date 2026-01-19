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

[Table("SHIPPING_INFOS")]
public partial class ShippingInfo : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("USER_ID")]
	public int UserId { get; set; }
	
	[Column("RECIPIENT_NAME")]
	[StringLength(250)]
	public string RecipientName { get; set; }
	
	[Column("ADDRESS_LINE")]
	[StringLength(250)]
	public string AddressLine { get; set; }
	
	[Column("CITY")]
	[StringLength(250)]
	public string City { get; set; }
	
	[Column("DISTRICT")]
	[StringLength(250)]
	public string District { get; set; }
	
	[Column("POSTAL_CODE")]
	[StringLength(250)]
	public string PostalCode { get; set; }
	
	[Column("IS_DEFAULT")]
	public bool IsDefault { get; set; }
	
	[Column("COUNTRY")]
	[StringLength(250)]
	public string Country { get; set; }
	
	[Column("PHONE")]
	[StringLength(250)]
	public string Phone { get; set; }
	
	[InverseProperty("ShippingInfo")]
	public virtual ICollection<Order> Orders { get; } = new List<Order>();

}

