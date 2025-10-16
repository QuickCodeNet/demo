using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.ApartmentManageModule.Domain.Enums;

public enum RelationshipType{
	[Description("Ev Sahibi")]
	Owner,
	[Description("Kiracı")]
	Tenant,
	[Description("Aile Üyesi")]
	FamilyMember,
	[Description("Misafir")]
	Guest
}
