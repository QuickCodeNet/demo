using QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.UserManagerModule
{
    public class AspNetUserClaimData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public AspNetUserClaimDto SelectedItem { get; set; }
        public List<AspNetUserClaimDto> List { get; set; }
    }

    public static partial class AspNetUserClaimExtensions
    {
        public static string GetKey(this AspNetUserClaimDto dto)
        {
            return $"{dto.Id}";
        }
    }
}