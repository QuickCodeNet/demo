using QuickCode.Demo.Common.Nswag.Clients.ProductCatalogModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.ProductCatalogModule
{
    public class AttributeValueData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public AttributeValueDto SelectedItem { get; set; }
        public List<AttributeValueDto> List { get; set; }
    }

    public static partial class AttributeValueExtensions
    {
        public static string GetKey(this AttributeValueDto dto)
        {
            return $"{dto.Id}";
        }
    }
}