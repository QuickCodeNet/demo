//------------------------------------------------------------------------------ 
// <auto-generated> 
// This code was generated by QuickCode. 
// Runtime Version:1.0
// 
// Changes to this file may cause incorrect behavior and will be lost if 
// the code is regenerated. 
// </auto-generated> 
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Models;
using QuickCode.Demo.Portal.Models.UserManagerModule;
using QuickCode.Demo.Portal.Helpers;
using Microsoft.AspNetCore.Mvc;
using UserManagerModuleContracts = QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using QuickCode.Demo.Portal.Helpers.Authorization;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using AutoRest.Core.Utilities.Collections;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QuickCode.Demo.Portal.Controllers.UserManagerModule
{
    public partial class TopicWorkflowsController : BaseController
    {
        [Route("List/{kafkaEventId}")]
        public async Task<IActionResult> List(int kafkaEventId)
        {
            var model = GetModel<TopicWorkflowsData>();
            model.PageSize = pageSize;
            model.CurrentPage = 1;
            model.NumberOfRecord = (await pageClient.CountAsync());
            model.TotalPage = (model.NumberOfRecord / model.PageSize) +
                              (model.NumberOfRecord % model.PageSize == 0 ? 0 : 1);
            model.ComboList = await FillPageComboBoxes(model.ComboList, kafkaEventId);
            var listResponse = (await pageClient.GetWorkflowsAsync(kafkaEventId));
            model.List = mapper.Map<List<TopicWorkflowsObj>>(listResponse.ToList());
            SetModelBinder(ref model);
            //return View("List", model);
            return PartialView("ListModal", model);
        }

        [Route("List/{kafkaEventId}")]
        [HttpPost]
        public async Task<IActionResult> List(TopicWorkflowsData model, int kafkaEventId)
        {
            ModelBinder(ref model);
            model.PageSize = pageSize;
            model.NumberOfRecord = (await pageClient.CountAsync());
            model.TotalPage = (model.NumberOfRecord / model.PageSize) +
                              (model.NumberOfRecord % model.PageSize == 0 ? 0 : 1);
            if (model.CurrentPage == Int32.MaxValue)
            {
                model.CurrentPage = model.TotalPage;
            }

            var listResponse = (await pageClient.GetWorkflowsAsync(kafkaEventId));
            model.List = mapper.Map<List<TopicWorkflowsObj>>(listResponse.ToList());
            SetModelBinder(ref model);
            model.SelectedItem = new TopicWorkflowsObj();
            return View("List", model);
        }

        private async Task<Dictionary<string, IEnumerable<SelectListItem>>> FillPageComboBoxes(
            Dictionary<string, IEnumerable<SelectListItem>> comboBoxList, int kafkaEventId)
        {
            comboBoxList.Clear();
            comboBoxList.AddRange(await FillComboBoxAsync("KafkaEvents",
                () => pageKafkaEventsClient.KafkaEventsGetAsync(), item => item.Id == kafkaEventId));
            return comboBoxList;
        }
    }
}