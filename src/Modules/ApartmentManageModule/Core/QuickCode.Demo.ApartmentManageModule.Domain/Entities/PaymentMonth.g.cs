using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.ApartmentManageModule.Domain;
using QuickCode.Demo.Common;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("PAYMENT_MONTHS")]
public partial class PaymentMonth  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[InverseProperty("Month")]
	public virtual ICollection<ApartmentFeePlan> ApartmentFeePlans { get; } = new List<ApartmentFeePlan>();


	[InverseProperty("Month")]
	public virtual ICollection<FlatPayment> FlatPayments { get; } = new List<FlatPayment>();


	[InverseProperty("Month")]
	public virtual ICollection<CommonExpense> CommonExpenses { get; } = new List<CommonExpense>();

}

