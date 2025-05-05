using QuickCode.Demo.Common.Nswag.Clients.EmailManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.EmailManagerModule
{
    public class EmailSendersData
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public EmailSendersDto SelectedItem { get; set; }

        public Dictionary<string, IEnumerable<SelectListItem>> ComboList = new Dictionary<string, IEnumerable<SelectListItem>>();
        public List<EmailSendersDto> List { get; set; }
    }

    public static partial class EmailSendersExtensions
    {
        public static string GetKey(this EmailSendersDto dto)
        {
            return $"{dto.Id}";
        }
    }
}