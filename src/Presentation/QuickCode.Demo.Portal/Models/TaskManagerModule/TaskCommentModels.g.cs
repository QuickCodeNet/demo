using QuickCode.Demo.Common.Nswag.Clients.TaskManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.TaskManagerModule
{
    public class TaskCommentData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public TaskCommentDto SelectedItem { get; set; }
        public List<TaskCommentDto> List { get; set; }
    }

    public static partial class TaskCommentExtensions
    {
        public static string GetKey(this TaskCommentDto dto)
        {
            return $"{dto.Id}";
        }
    }
}