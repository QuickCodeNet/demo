using QuickCode.Demo.Common.Nswag.Clients.OnlineShopModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.OnlineShopModule
{
    public class AuditLogData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public AuditLogDto SelectedItem { get; set; }
        public List<AuditLogDto> List { get; set; }
    }

    public static partial class AuditLogExtensions
    {
        public static string GetKey(this AuditLogDto dto)
        {
            return $"{dto.Id}";
        }
    }
}