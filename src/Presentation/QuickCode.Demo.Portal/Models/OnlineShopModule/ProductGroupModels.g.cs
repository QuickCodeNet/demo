using QuickCode.Demo.Common.Nswag.Clients.OnlineShopModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.OnlineShopModule
{
    public class ProductGroupData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public ProductGroupDto SelectedItem { get; set; }
        public List<ProductGroupDto> List { get; set; }
    }

    public static partial class ProductGroupExtensions
    {
        public static string GetKey(this ProductGroupDto dto)
        {
            return $"{dto.Id}";
        }

        public static List<string> GetImageColumnNames(this ProductGroupDto dto) => new()
        {
            "ImageUrl"
        };
    }
}