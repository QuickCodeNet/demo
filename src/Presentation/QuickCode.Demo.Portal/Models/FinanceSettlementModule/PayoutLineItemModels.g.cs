using QuickCode.Demo.Common.Nswag.Clients.FinanceSettlementModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.FinanceSettlementModule
{
    public class PayoutLineItemData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public PayoutLineItemDto SelectedItem { get; set; }
        public List<PayoutLineItemDto> List { get; set; }
    }

    public static partial class PayoutLineItemExtensions
    {
        public static string GetKey(this PayoutLineItemDto dto)
        {
            return $"{dto.Id}";
        }
    }
}