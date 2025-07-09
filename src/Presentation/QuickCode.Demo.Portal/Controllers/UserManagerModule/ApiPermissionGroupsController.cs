using System;
using System.Collections.Generic;
using System.Linq;
using QuickCode.Demo.Portal.Models;
using QuickCode.Demo.Portal.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using QuickCode.Demo.Portal.Helpers.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace QuickCode.Demo.Portal.Controllers.UserManagerModule
{
    [Permission("UserManagerModuleApiPermissionGroups")]
    public partial class ApiPermissionGroupsController : BaseController
    {
        [Route("GetModulePermissions")]
        [HttpGet]
        public async Task<IActionResult> GetModulePermissions()
        {
            var model = GetModel<GetApiPermissionGroupData>();
            var groups = await pagePermissionGroupsClient.PermissionGroupsListAsync();
            model.SelectedGroupId = groups.First().Id;
            model.ComboList = await FillPageComboBoxes(model.ComboList);
            model.Items = (await pageApiMethodDefinitionsClient.ApiMethodDefinitionsGetApiPermissionsAsync(model.SelectedGroupId)).Value;
            SetModelBinder(ref model);
            return View("ApiPermissionGroups", model);
        }

        [Route("GetModulePermissions")]
        [HttpPost]
        public async Task<IActionResult> GetModulePermissions(GetApiPermissionGroupData model)
        {
            ModelBinder(ref model);
            model.Items = (await pageApiMethodDefinitionsClient.ApiMethodDefinitionsGetApiPermissionsAsync(model.SelectedGroupId)).Value;
            SetModelBinder(ref model);
            return View("ApiPermissionGroups", model);
        }

        [Route("UpdatePermission")]
        [HttpPost]
        public async Task<JsonResult> UpdatePermission(UpdateGroupAuthorizationApiRequestData model)
        {
            var result = await pageApiMethodDefinitionsClient.ApiMethodDefinitionsUpdateApiPermissionAsync(model);
            HttpContextAccessor.HttpContext!.Session.Remove("PortalPermissions");
            HttpContextAccessor.HttpContext!.Session.Remove("ApiPermissions");
            HttpContextAccessor.HttpContext!.Session.Remove("PortalPermissionGroups");
            HttpContextAccessor.HttpContext!.Session.Remove("ApiPermissionGroups");
            HttpContextAccessor.HttpContext!.Session.Remove("PortalPermissionTypes");
            HttpContextAccessor.HttpContext!.Session.Remove("MenuItems");
            return Json(result);
        }

    }
}

