using QuickCode.Demo.Common.Nswag.Clients.SmsManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.SmsManagerModule
{
    public class SmsSendersData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public SmsSendersDto SelectedItem { get; set; }
        public List<SmsSendersDto> List { get; set; }
    }

    public static partial class SmsSendersExtensions
    {
        public static string GetKey(this SmsSendersDto dto)
        {
            return $"{dto.Id}";
        }
    }
}