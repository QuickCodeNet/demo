using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuickCode.Demo.OnlineShopModule.Domain.Enums;

public enum PaymentMethod{
	[Description("Kredi Kartı")]
	CreditCard,
	[Description("Havale/EFT")]
	BankTransfer,
	[Description("Kapıda Ödeme")]
	CashOnDelivery,
	[Description("Dijital Cüzdan")]
	DigitalWallet
}
