using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.ApartmentManageModule.Domain;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Auditing;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Entities;

[Table("PAYMENT_YEARS")]
public partial class PaymentYear : IAuditableEntity 
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("ID")]
	public int Id { get; set; }
	
	[Column("NAME")]
	[StringLength(250)]
	public string Name { get; set; }
	
	[InverseProperty("Year")]
	public virtual ICollection<ApartmentFeePlan> ApartmentFeePlans { get; } = new List<ApartmentFeePlan>();


	[InverseProperty("Year")]
	public virtual ICollection<FlatPayment> FlatPayments { get; } = new List<FlatPayment>();


	[InverseProperty("Year")]
	public virtual ICollection<CommonExpense> CommonExpenses { get; } = new List<CommonExpense>();

}

