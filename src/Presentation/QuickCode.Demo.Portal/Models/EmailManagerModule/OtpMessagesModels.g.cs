using QuickCode.Demo.Common.Nswag.Clients.EmailManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.EmailManagerModule
{
    public class OtpMessagesData
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public OtpMessagesDto SelectedItem { get; set; }

        public Dictionary<string, IEnumerable<SelectListItem>> ComboList = new Dictionary<string, IEnumerable<SelectListItem>>();
        public List<OtpMessagesDto> List { get; set; }
    }

    public static partial class OtpMessagesExtensions
    {
        public static string GetKey(this OtpMessagesDto dto)
        {
            return $"{dto.Id}";
        }
    }
}