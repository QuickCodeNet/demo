using QuickCode.Demo.Common.Nswag.Clients.OrderManagementModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.OrderManagementModule
{
    public class ReturnRequestData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public ReturnRequestDto SelectedItem { get; set; }
        public List<ReturnRequestDto> List { get; set; }
    }

    public static partial class ReturnRequestExtensions
    {
        public static string GetKey(this ReturnRequestDto dto)
        {
            return $"{dto.Id}";
        }
    }
}