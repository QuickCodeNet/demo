using QuickCode.Demo.Common.Nswag.Clients.EmailManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.EmailManagerModule
{
    public class MessageLogData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public MessageLogDto SelectedItem { get; set; }
        public List<MessageLogDto> List { get; set; }
    }

    public static partial class MessageLogExtensions
    {
        public static string GetKey(this MessageLogDto dto)
        {
            return $"{dto.Id}";
        }
    }
}