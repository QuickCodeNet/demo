using QuickCode.Demo.Common.Nswag.Clients.ApartmentManageModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.ApartmentManageModule
{
    public class PaymentMonthsData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public PaymentMonthsDto SelectedItem { get; set; }
        public List<PaymentMonthsDto> List { get; set; }
    }

    public static partial class PaymentMonthsExtensions
    {
        public static string GetKey(this PaymentMonthsDto dto)
        {
            return $"{dto.Id}";
        }
    }
}