using QuickCode.Demo.Common.Nswag.Clients.EmptyTestModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.EmptyTestModule
{
    public class CustomTableData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public CustomTableDto SelectedItem { get; set; }
        public List<CustomTableDto> List { get; set; }
    }

    public static partial class CustomTableExtensions
    {
        public static string GetKey(this CustomTableDto dto)
        {
            return $"{dto.Id}";
        }
    }
}