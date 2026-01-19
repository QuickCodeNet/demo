using QuickCode.Demo.Common.Nswag.Clients.OnlineShopModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.OnlineShopModule
{
    public class CompanyInfoData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public CompanyInfoDto SelectedItem { get; set; }
        public List<CompanyInfoDto> List { get; set; }
    }

    public static partial class CompanyInfoExtensions
    {
        public static string GetKey(this CompanyInfoDto dto)
        {
            return $"{dto.Id}";
        }

        public static List<string> GetImageColumnNames(this CompanyInfoDto dto) => new()
        {
            "CompanyIconUrl",
            "CompanyLogoUrl"
        };
    }
}