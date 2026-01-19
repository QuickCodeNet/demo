using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.EmailManagerModule.Domain.Enums;

public enum BlacklistReasonType{
	Spam,
	Unsubscribed,
	InvalidAddress,
	Bounced,
	ManualBlock
}
