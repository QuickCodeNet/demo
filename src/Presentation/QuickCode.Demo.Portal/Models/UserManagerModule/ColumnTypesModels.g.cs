using QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.UserManagerModule
{
    public class ColumnTypesData
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public ColumnTypesDto SelectedItem { get; set; }

        public Dictionary<string, IEnumerable<SelectListItem>> ComboList = new Dictionary<string, IEnumerable<SelectListItem>>();
        public List<ColumnTypesDto> List { get; set; }
    }

    public static partial class ColumnTypesExtensions
    {
        public static string GetKey(this ColumnTypesDto dto)
        {
            return $"{dto.Id}";
        }
    }
}