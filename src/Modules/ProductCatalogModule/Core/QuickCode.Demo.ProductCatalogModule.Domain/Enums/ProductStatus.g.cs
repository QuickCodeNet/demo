using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickCode.Demo.ProductCatalogModule.Domain.Enums;

public enum ProductStatus{
	[Description("Product is being created and not yet visible")]
	Draft,
	[Description("Product submitted for review by marketplace admin")]
	PendingApproval,
	[Description("Product is live and available for sale")]
	Active,
	[Description("Product is not visible on the marketplace")]
	Inactive,
	[Description("Product is no longer sold")]
	Discontinued
}
