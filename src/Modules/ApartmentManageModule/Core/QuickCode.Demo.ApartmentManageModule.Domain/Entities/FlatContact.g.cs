using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.ApartmentManageModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("FLAT_CONTACTS")]
public partial class FlatContact : BaseSoftDeletable, IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("FLAT_ID")]
	public int FlatId { get; set; }
	
	[Column("CONTACT_ID")]
	public int ContactId { get; set; }
	
	[Column("RELATIONSHIP_TYPE", TypeName = "nvarchar(250)")]
	public RelationshipType RelationshipType { get; set; }
	
	[Column("START_DATE")]
	public DateTime StartDate { get; set; }
	
	[Column("END_DATE")]
	public DateTime? EndDate { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[Column("CREATED_DATE")]
	public DateTime CreatedDate { get; set; }
	
	[ForeignKey("FlatId")]
	[InverseProperty("FlatContacts")]
	public virtual Flat Flat { get; set; } = null!;


	[ForeignKey("ContactId")]
	[InverseProperty("FlatContacts")]
	public virtual Contact Contact { get; set; } = null!;

}

