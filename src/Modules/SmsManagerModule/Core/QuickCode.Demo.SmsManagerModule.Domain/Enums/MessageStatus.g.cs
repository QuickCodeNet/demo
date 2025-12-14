using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.SmsManagerModule.Domain.Enums;

public enum MessageStatus{
	Pending,
	Sent,
	Failed,
	Expired,
	InvalidRecipient,
	ServiceUnavailable,
	QuotaExceeded,
	Timeout,
	Unknown
}
