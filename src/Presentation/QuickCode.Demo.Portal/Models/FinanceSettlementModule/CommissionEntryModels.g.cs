using QuickCode.Demo.Common.Nswag.Clients.FinanceSettlementModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.FinanceSettlementModule
{
    public class CommissionEntryData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public CommissionEntryDto SelectedItem { get; set; }
        public List<CommissionEntryDto> List { get; set; }
    }

    public static partial class CommissionEntryExtensions
    {
        public static string GetKey(this CommissionEntryDto dto)
        {
            return $"{dto.Id}";
        }
    }
}