using QuickCode.Demo.Common.Nswag.Clients.SellerManagementModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.SellerManagementModule
{
    public class SellerData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public SellerDto SelectedItem { get; set; }
        public List<SellerDto> List { get; set; }
    }

    public static partial class SellerExtensions
    {
        public static string GetKey(this SellerDto dto)
        {
            return $"{dto.Id}";
        }
    }
}