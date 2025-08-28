using QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.UserManagerModule
{
    public class PortalPermissionsData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public PortalPermissionsDto SelectedItem { get; set; }
        public List<PortalPermissionsDto> List { get; set; }
    }

    public static partial class PortalPermissionsExtensions
    {
        public static string GetKey(this PortalPermissionsDto dto)
        {
            return $"{dto.Name}";
        }
    }
}