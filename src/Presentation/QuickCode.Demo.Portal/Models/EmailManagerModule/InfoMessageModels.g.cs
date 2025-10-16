using QuickCode.Demo.Common.Nswag.Clients.EmailManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.EmailManagerModule
{
    public class InfoMessageData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public InfoMessageDto SelectedItem { get; set; }
        public List<InfoMessageDto> List { get; set; }
    }

    public static partial class InfoMessageExtensions
    {
        public static string GetKey(this InfoMessageDto dto)
        {
            return $"{dto.Id}";
        }
    }
}