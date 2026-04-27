using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickCode.Demo.SellerManagementModule.Domain.Enums;

public enum DocumentType{
	[Description("National ID or Passport")]
	Identity,
	[Description("Official business registration document")]
	BusinessRegistration,
	[Description("Tax identification document")]
	TaxId,
	[Description("Bank account ownership proof")]
	BankStatement
}
