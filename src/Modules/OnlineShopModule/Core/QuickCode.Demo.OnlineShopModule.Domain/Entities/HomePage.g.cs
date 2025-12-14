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

[Table("HOME_PAGES")]
public partial class HomePage : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("MAIN_TITLE")]
	[StringLength(250)]
	public string MainTitle { get; set; }
	
	[Column("TITLE")]
	[StringLength(250)]
	public string Title { get; set; }
	
	[Column("TITLE_DESCRIPTION")]
	[StringLength(250)]
	public string TitleDescription { get; set; }
	
	[Column("TITLE_IMAGE_URL")]
	[StringLength(500)]
	public string TitleImageUrl { get; set; }
	}

