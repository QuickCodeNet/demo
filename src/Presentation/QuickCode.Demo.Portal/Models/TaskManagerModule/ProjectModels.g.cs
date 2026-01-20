using QuickCode.Demo.Common.Nswag.Clients.TaskManagerModuleApi.Contracts;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Helpers;

namespace QuickCode.Demo.Portal.Models.TaskManagerModule
{
    public class ProjectData : BaseComboBoxModel
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumberOfRecord { get; set; }
        public string SelectedKey { get; set; }
        public ProjectDto SelectedItem { get; set; }
        public List<ProjectDto> List { get; set; }
    }

    public static partial class ProjectExtensions
    {
        public static string GetKey(this ProjectDto dto)
        {
            return $"{dto.Id}";
        }
    }
}