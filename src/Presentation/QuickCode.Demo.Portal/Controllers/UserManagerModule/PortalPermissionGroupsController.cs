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
    [Permission("UserManagerModulePortalPermissions")]
    public partial class PortalPermissionGroupsController : BaseController
    {
        [Route("GetGroupPermissions")]
        [HttpGet]
        public async Task<IActionResult> GetGroupPermissions()
        {
            var model = GetModel<GetPortalPermissionGroupData>();
            var groups = await pagePermissionGroupClient.PermissionGroupsListAsync();
            model.SelectedGroupName = groups.First().Name;
            model.ComboList = await FillPageComboBoxes(model.ComboList);
            model.Items = await pagePortalPermissionClient.PortalPermissionsGetPortalPermissionsAsync(model.SelectedGroupName);
            SetModelBinder(ref model);
            return View("PortalPermissionGroups", model);
        }

        [Route("GetGroupPermissions")]
        [HttpPost]
        public async Task<IActionResult> GetGroupPermissions(GetPortalPermissionGroupData model)
        {
            ModelBinder(ref model);
            model.Items = await pagePortalPermissionClient.PortalPermissionsGetPortalPermissionsAsync(model.SelectedGroupName);
            SetModelBinder(ref model);
            return View("PortalPermissionGroups", model);
        }

        [Route("UpdatePermission")]
        [HttpPost]
        public async Task<JsonResult> UpdatePermission(UpdateGroupAuthorizationRequestData model)
        {
            var result = await pagePortalPermissionClient.PortalPermissionsUpdatePortalPermissionAsync(model);
            HttpContextAccessor.HttpContext!.Session.Remove("PortalPermissions");
            HttpContextAccessor.HttpContext!.Session.Remove("PortalPermissionGroups");
            HttpContextAccessor.HttpContext!.Session.Remove("PortalPermissionTypes");
            HttpContextAccessor.HttpContext!.Session.Remove("MenuItems");
            return Json(result);
        }

    }
}

