using QuickCode.Demo.Common.Nswag.Clients.OnlineShopModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.OnlineShopModule
{
    public class ProductTypeData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public ProductTypeDto SelectedItem { get; set; }
        public List<ProductTypeDto> List { get; set; }
    }

    public static partial class ProductTypeExtensions
    {
        public static string GetKey(this ProductTypeDto dto)
        {
            return $"{dto.Id}";
        }

        public static List<string> GetImageColumnNames(this ProductTypeDto dto) => new()
        {
            "ImageUrl"
        };
    }
}