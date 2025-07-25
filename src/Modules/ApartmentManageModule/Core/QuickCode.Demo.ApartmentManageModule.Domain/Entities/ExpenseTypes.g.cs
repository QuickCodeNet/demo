using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("EXPENSE_TYPES")]
public partial class ExpenseTypes  : BaseSoftDeletable 
{
	[Key]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[Column("DESCRIPTION")]
	[StringLength(250)]
	public string? Description { get; set; }
	
	[Column("IS_ACTIVE")]
	public bool IsActive { get; set; }
	
	[InverseProperty("ExpenseType")]
	public virtual ICollection<CommonExpenses> CommonExpenses { get; } = new List<CommonExpenses>();

}

