using QuickCode.Demo.Common.Nswag.Clients.FinanceSettlementModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.FinanceSettlementModule
{
    public class SellerPayoutData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public SellerPayoutDto SelectedItem { get; set; }
        public List<SellerPayoutDto> List { get; set; }
    }

    public static partial class SellerPayoutExtensions
    {
        public static string GetKey(this SellerPayoutDto dto)
        {
            return $"{dto.Id}";
        }
    }
}