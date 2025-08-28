using QuickCode.Demo.UserManagerModule.Application.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickCode.Demo.UserManagerModule.Application.Features;
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using QuickCode.Demo.UserManagerModule.Application.Features.Queries.PortalPermissionGroups;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class PortalPermissionsController 
    {
	    [HttpGet("get-portal-permissions/{permissionGroupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortalPermissionGroupList))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissions(string permissionGroupName)
        {
            var response = await mediator.Send(new PortalPermissionGroupsGetItemsQuery(permissionGroupName));
            return Ok(response.Value);
        }

        [HttpPost("update-portal-permission")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdatePortalPermission(UpdatePortalPermissionGroupRequest request)
        {
            var response = await mediator.Send(new PortalPermissionGroupsUpdateCommand(
                request.PortalPermissionName,
                request.PermissionGroupName,
                request.PortalPermissionTypeId,
                new PortalPermissionGroupsDto()
                {
                    PermissionGroupName = request.PermissionGroupName,
                    PortalPermissionName = request.PortalPermissionName,
                    PortalPermissionTypeId = request.PortalPermissionTypeId,
                    IsActive = request.Value == 1
                }));

            return Ok(response.Code == 0);
        }
    }
}

