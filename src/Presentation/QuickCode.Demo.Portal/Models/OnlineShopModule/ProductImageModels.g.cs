using QuickCode.Demo.Common.Nswag.Clients.OnlineShopModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.OnlineShopModule
{
    public class ProductImageData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public ProductImageDto SelectedItem { get; set; }
        public List<ProductImageDto> List { get; set; }
    }

    public static partial class ProductImageExtensions
    {
        public static string GetKey(this ProductImageDto dto)
        {
            return $"{dto.Id}";
        }

        public static List<string> GetImageColumnNames(this ProductImageDto dto) => new()
        {
            "ImageUrl"
        };
    }
}