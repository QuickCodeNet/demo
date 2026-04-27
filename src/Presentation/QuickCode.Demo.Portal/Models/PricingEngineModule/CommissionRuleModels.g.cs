using QuickCode.Demo.Common.Nswag.Clients.PricingEngineModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.PricingEngineModule
{
    public class CommissionRuleData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public CommissionRuleDto SelectedItem { get; set; }
        public List<CommissionRuleDto> List { get; set; }
    }

    public static partial class CommissionRuleExtensions
    {
        public static string GetKey(this CommissionRuleDto dto)
        {
            return $"{dto.Id}";
        }
    }
}