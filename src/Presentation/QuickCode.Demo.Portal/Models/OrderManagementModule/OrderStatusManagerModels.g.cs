using QuickCode.Demo.Common.Nswag.Clients.OrderManagementModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.OrderManagementModule
{
    public class OrderStatusManagerData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public OrderStatusManagerDto SelectedItem { get; set; }
        public List<OrderStatusManagerDto> List { get; set; }
    }

    public static partial class OrderStatusManagerExtensions
    {
        public static string GetKey(this OrderStatusManagerDto dto)
        {
            return $"{dto.Id}";
        }
    }
}