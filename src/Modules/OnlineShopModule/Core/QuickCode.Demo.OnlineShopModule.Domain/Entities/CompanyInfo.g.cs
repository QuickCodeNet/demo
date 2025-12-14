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

[Table("COMPANY_INFOS")]
public partial class CompanyInfo : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("ADDRESS")]
	[StringLength(250)]
	public string Address { get; set; }
	
	[Column("CITY")]
	[StringLength(250)]
	public string City { get; set; }
	
	[Column("COUNTRY")]
	[StringLength(250)]
	public string Country { get; set; }
	
	[Column("ABOUT_US")]
	[StringLength(250)]
	public string AboutUs { get; set; }
	
	[Column("EMAIL")]
	[StringLength(250)]
	public string Email { get; set; }
	
	[Column("PHONE")]
	[StringLength(250)]
	public string Phone { get; set; }
	
	[Column("LONGITUDE")]
	public decimal Longitude { get; set; }
	
	[Column("LATITUDE")]
	public decimal Latitude { get; set; }
	
	[Column("COMPANY_ICON_URL")]
	[StringLength(500)]
	public string CompanyIconUrl { get; set; }
	
	[Column("COMPANY_LOGO_URL")]
	[StringLength(500)]
	public string CompanyLogoUrl { get; set; }
	}

