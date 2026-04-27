using QuickCode.Demo.Common.Nswag.Clients.SellerManagementModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.SellerManagementModule
{
    public class SellerPerformanceReviewData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public SellerPerformanceReviewDto SelectedItem { get; set; }
        public List<SellerPerformanceReviewDto> List { get; set; }
    }

    public static partial class SellerPerformanceReviewExtensions
    {
        public static string GetKey(this SellerPerformanceReviewDto dto)
        {
            return $"{dto.Id}";
        }
    }
}