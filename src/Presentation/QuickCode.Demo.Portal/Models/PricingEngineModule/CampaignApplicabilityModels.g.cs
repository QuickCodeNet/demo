using QuickCode.Demo.Common.Nswag.Clients.PricingEngineModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.PricingEngineModule
{
    public class CampaignApplicabilityData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public CampaignApplicabilityDto SelectedItem { get; set; }
        public List<CampaignApplicabilityDto> List { get; set; }
    }

    public static partial class CampaignApplicabilityExtensions
    {
        public static string GetKey(this CampaignApplicabilityDto dto)
        {
            return $"{dto.CampaignId}|{dto.Scope}|{dto.ScopeId}";
        }
    }
}