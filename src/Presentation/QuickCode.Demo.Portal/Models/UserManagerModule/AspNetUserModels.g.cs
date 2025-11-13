using QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.UserManagerModule
{
    public class AspNetUserData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public AspNetUserDto SelectedItem { get; set; }
        public List<AspNetUserDto> List { get; set; }
    }

    public static partial class AspNetUserExtensions
    {
        public static string GetKey(this AspNetUserDto dto)
        {
            return $"{dto.Id}";
        }
    }
}