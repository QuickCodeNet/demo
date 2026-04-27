using QuickCode.Demo.Common.Nswag.Clients.SellerManagementModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.SellerManagementModule
{
    public class SellerDocumentData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public SellerDocumentDto SelectedItem { get; set; }
        public List<SellerDocumentDto> List { get; set; }
    }

    public static partial class SellerDocumentExtensions
    {
        public static string GetKey(this SellerDocumentDto dto)
        {
            return $"{dto.Id}";
        }
    }
}