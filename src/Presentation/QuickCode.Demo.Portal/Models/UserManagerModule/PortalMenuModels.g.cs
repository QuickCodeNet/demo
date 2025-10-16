﻿using QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.UserManagerModule
{
    public class PortalMenuData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public PortalMenuDto SelectedItem { get; set; }
        public List<PortalMenuDto> List { get; set; }
    }

    public static partial class PortalMenuExtensions
    {
        public static string GetKey(this PortalMenuDto dto)
        {
            return $"{dto.Key}";
        }
    }
}