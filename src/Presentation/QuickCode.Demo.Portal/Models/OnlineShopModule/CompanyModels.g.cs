using QuickCode.Demo.Common.Nswag.Clients.OnlineShopModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.OnlineShopModule
{
    public class CompanyData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public CompanyDto SelectedItem { get; set; }
        public List<CompanyDto> List { get; set; }
    }

    public static partial class CompanyExtensions
    {
        public static string GetKey(this CompanyDto dto)
        {
            return $"{dto.Id}";
        }
    }
}