using QuickCode.Demo.Common.Nswag.Clients.OnlineShopModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.OnlineShopModule
{
    public class CartItemData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public CartItemDto SelectedItem { get; set; }
        public List<CartItemDto> List { get; set; }
    }

    public static partial class CartItemExtensions
    {
        public static string GetKey(this CartItemDto dto)
        {
            return $"{dto.Id}";
        }
    }
}