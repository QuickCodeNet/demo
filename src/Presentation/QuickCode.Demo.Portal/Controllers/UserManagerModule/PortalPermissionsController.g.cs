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
    [Permission("UserManagerModulePortalPermissions")]
    [Area("UserManagerModule")]
    [Route("UserManagerModulePortalPermissions")]
    public partial class PortalPermissionsController : BaseController
    {
        private int pageSize = 20;
        private readonly UserManagerModuleContracts.IPortalPermissionsClient pageClient;
        public PortalPermissionsController(UserManagerModuleContracts.IPortalPermissionsClient pageClient, UserManagerModuleContracts.ITableComboboxSettingsClient tableComboboxSettingsClient, IHttpContextAccessor httpContextAccessor, IMapper mapper, IMemoryCache cache) : base(tableComboboxSettingsClient, httpContextAccessor, mapper, cache)
        {
            this.pageClient = pageClient;
        }

        [ResponseCache(VaryByQueryKeys = new[] { "ic" }, Duration = 30)]
        public async Task<IActionResult> GetImage(string ic)
        {
            return await GetImageResult(pageClient, ic);
        }

        [Route("List")]
        public async Task<IActionResult> List()
        {
            var model = GetModel<PortalPermissionsData>();
            model.PageSize = pageSize;
            model.CurrentPage = 1;
            model.NumberOfRecord = (await pageClient.CountAsync());
            model.TotalPage = (model.NumberOfRecord / model.PageSize) + (model.NumberOfRecord % model.PageSize == 0 ? 0 : 1);
            var listResponse = (await pageClient.PortalPermissionsGetAsync(model.CurrentPage, model.PageSize));
            model.List = mapper.Map<List<PortalPermissionsObj>>(listResponse.ToList());
            SetModelBinder(ref model);
            return View("List", model);
        }

        [Route("List")]
        [HttpPost]
        public async Task<IActionResult> List(PortalPermissionsData model)
        {
            ModelBinder(ref model);
            model.PageSize = pageSize;
            model.NumberOfRecord = (await pageClient.CountAsync());
            model.TotalPage = (model.NumberOfRecord / model.PageSize) + (model.NumberOfRecord % model.PageSize == 0 ? 0 : 1);
            if (model.CurrentPage == Int32.MaxValue)
            {
                model.CurrentPage = model.TotalPage;
            }

            var listResponse = (await pageClient.PortalPermissionsGetAsync(model.CurrentPage, model.PageSize));
            model.List = mapper.Map<List<PortalPermissionsObj>>(listResponse.ToList());
            SetModelBinder(ref model);
            model.SelectedItem = new PortalPermissionsObj();
            return View("List", model);
        }

        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert(PortalPermissionsData model)
        {
            ModelBinder(ref model);
            var selected = mapper.Map<UserManagerModuleContracts.PortalPermissionsDto>(model.SelectedItem);
            var result = await pageClient.PortalPermissionsPostAsync(selected);
            ClearCache();
            SetModelBinder(ref model);
            return Ok(result);
        }

        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update(PortalPermissionsData model)
        {
            ModelBinder(ref model);
            var request = mapper.Map<UserManagerModuleContracts.PortalPermissionsDto>(model.SelectedItem);
            var result = await pageClient.PortalPermissionsPutAsync(request.Id, request);
            ClearCache();
            SetModelBinder(ref model);
            return Ok(result);
        }

        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(PortalPermissionsData model)
        {
            ModelBinder(ref model);
            var request = model.SelectedItem;
            var result = await pageClient.PortalPermissionsDeleteAsync(request.Id);
            ClearCache();
            SetModelBinder(ref model);
            return Ok(result);
        }

        [Route("InsertItem")]
        public IActionResult InsertItem(PortalPermissionsData model)
        {
            ModelState.Clear();
            ModelBinder(ref model);
            SetModelBinder(ref model);
            model.SelectedItem = new PortalPermissionsObj();
            return PartialView("Insert", model);
        }

        [Route("DetailItem")]
        public async Task<IActionResult> DetailItem(PortalPermissionsData model)
        {
            ModelBinder(ref model);
            if (model.List == null)
            {
                model = await FillModel(model);
            }

            model.SelectedItem = model.List.Find(i => i._Key == model.SelectedKey);
            SetModelBinder(ref model);
            return PartialView("Detail", model);
        }

        [Route("UpdateItem")]
        [HttpPost]
        public async Task<IActionResult> UpdateItem(PortalPermissionsData model)
        {
            ModelState.Clear();
            ModelBinder(ref model);
            if (model.List == null)
            {
                model = await FillModel(model);
            }

            model.SelectedItem = model.List.Find(i => i._Key == model.SelectedKey);
            SetModelBinder(ref model);
            return PartialView("Update", model);
        }

        [Route("DeleteItem")]
        [HttpPost]
        public async Task<IActionResult> DeleteItem(PortalPermissionsData model)
        {
            ModelBinder(ref model);
            if (model.List == null)
            {
                model = await FillModel(model);
            }

            model.SelectedItem = model.List.Find(i => i._Key == model.SelectedKey);
            SetModelBinder(ref model);
            return PartialView("Delete", model);
        }

        private async Task<PortalPermissionsData> FillModel(PortalPermissionsData model)
        {
            model.PageSize = pageSize;
            model.NumberOfRecord = (await pageClient.CountAsync());
            model.TotalPage = (model.NumberOfRecord / model.PageSize) + (model.NumberOfRecord % model.PageSize == 0 ? 0 : 1);
            var listResponse = (await pageClient.PortalPermissionsGetAsync(model.CurrentPage, model.PageSize));
            model.List = mapper.Map<List<PortalPermissionsObj>>(listResponse.ToList());
            return model;
        }

        public void ClearCache()
        {
            var cacheKey = $"PortalPermissionsData";
            cache.Remove(cacheKey);
        }
    }
}