using QuickCode.Demo.Common.Nswag.Clients.OnlineShopModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.OnlineShopModule
{
    public class ShippingInfoData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public ShippingInfoDto SelectedItem { get; set; }
        public List<ShippingInfoDto> List { get; set; }
    }

    public static partial class ShippingInfoExtensions
    {
        public static string GetKey(this ShippingInfoDto dto)
        {
            return $"{dto.Id}";
        }
    }
}