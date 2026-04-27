using QuickCode.Demo.Common.Nswag.Clients.FinanceSettlementModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.FinanceSettlementModule
{
    public class TransactionLedgerData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public TransactionLedgerDto SelectedItem { get; set; }
        public List<TransactionLedgerDto> List { get; set; }
    }

    public static partial class TransactionLedgerExtensions
    {
        public static string GetKey(this TransactionLedgerDto dto)
        {
            return $"{dto.Id}";
        }
    }
}