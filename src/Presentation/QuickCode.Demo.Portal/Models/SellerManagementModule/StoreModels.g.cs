using QuickCode.Demo.Common.Nswag.Clients.SellerManagementModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.SellerManagementModule
{
    public class StoreData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public StoreDto SelectedItem { get; set; }
        public List<StoreDto> List { get; set; }
    }

    public static partial class StoreExtensions
    {
        public static string GetKey(this StoreDto dto)
        {
            return $"{dto.Id}";
        }

        public static List<string> GetImageColumnNames(this StoreDto dto) => new()
        {
            "LogoUrl"
        };
    }
}