using QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.UserManagerModule
{
    public class RefreshTokensData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public RefreshTokensDto SelectedItem { get; set; }
        public List<RefreshTokensDto> List { get; set; }
    }

    public static partial class RefreshTokensExtensions
    {
        public static string GetKey(this RefreshTokensDto dto)
        {
            return $"{dto.Id}";
        }
    }
}