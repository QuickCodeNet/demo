using QuickCode.Demo.Common.Nswag.Clients.SmsManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.SmsManagerModule
{
    public class OtpMessageLogData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public OtpMessageLogDto SelectedItem { get; set; }
        public List<OtpMessageLogDto> List { get; set; }
    }

    public static partial class OtpMessageLogExtensions
    {
        public static string GetKey(this OtpMessageLogDto dto)
        {
            return $"{dto.Id}";
        }
    }
}