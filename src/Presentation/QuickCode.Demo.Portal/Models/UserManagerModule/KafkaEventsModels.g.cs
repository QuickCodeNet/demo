using QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.UserManagerModule
{
    public class KafkaEventsData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public KafkaEventsDto SelectedItem { get; set; }
        public List<KafkaEventsDto> List { get; set; }
    }

    public static partial class KafkaEventsExtensions
    {
        public static string GetKey(this KafkaEventsDto dto)
        {
            return $"{dto.TopicName}";
        }
    }
}