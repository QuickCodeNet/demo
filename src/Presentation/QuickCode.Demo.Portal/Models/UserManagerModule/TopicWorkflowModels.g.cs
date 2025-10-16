﻿using QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.UserManagerModule
{
    public class TopicWorkflowData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public TopicWorkflowDto SelectedItem { get; set; }
        public List<TopicWorkflowDto> List { get; set; }
    }

    public static partial class TopicWorkflowExtensions
    {
        public static string GetKey(this TopicWorkflowDto dto)
        {
            return $"{dto.Id}";
        }
    }
}