using QuickCode.Demo.Common.Nswag.Clients.SmsManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.SmsManagerModule
{
    public class InfoMessagesData
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public InfoMessagesDto SelectedItem { get; set; }

        public Dictionary<string, IEnumerable<SelectListItem>> ComboList = new Dictionary<string, IEnumerable<SelectListItem>>();
        public List<InfoMessagesDto> List { get; set; }
    }

    public static partial class InfoMessagesExtensions
    {
        public static string GetKey(this InfoMessagesDto dto)
        {
            return $"{dto.Id}";
        }
    }
}