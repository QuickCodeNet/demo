using QuickCode.Demo.Common.Nswag.Clients.ApartmentManageModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.ApartmentManageModule
{
    public class ExpenseInstallmentsData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public ExpenseInstallmentsDto SelectedItem { get; set; }
        public List<ExpenseInstallmentsDto> List { get; set; }
    }

    public static partial class ExpenseInstallmentsExtensions
    {
        public static string GetKey(this ExpenseInstallmentsDto dto)
        {
            return $"{dto.Id}";
        }
    }
}