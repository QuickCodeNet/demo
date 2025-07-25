using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("PAYMENT_YEARS")]
public partial class PaymentYears  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[InverseProperty("Year")]
	public virtual ICollection<ApartmentFeePlans> ApartmentFeePlans { get; } = new List<ApartmentFeePlans>();


	[InverseProperty("Year")]
	public virtual ICollection<FlatPayments> FlatPayments { get; } = new List<FlatPayments>();


	[InverseProperty("Year")]
	public virtual ICollection<CommonExpenses> CommonExpenses { get; } = new List<CommonExpenses>();

}

