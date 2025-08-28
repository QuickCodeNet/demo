using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("PAYMENT_MONTHS")]
public partial class PaymentMonths  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[InverseProperty("Month")]
	public virtual ICollection<ApartmentFeePlans> ApartmentFeePlans { get; } = new List<ApartmentFeePlans>();


	[InverseProperty("Month")]
	public virtual ICollection<FlatPayments> FlatPayments { get; } = new List<FlatPayments>();


	[InverseProperty("Month")]
	public virtual ICollection<CommonExpenses> CommonExpenses { get; } = new List<CommonExpenses>();

}

