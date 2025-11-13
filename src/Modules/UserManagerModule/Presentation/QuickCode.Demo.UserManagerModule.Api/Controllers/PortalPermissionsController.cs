using QuickCode.Demo.UserManagerModule.Application.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickCode.Demo.UserManagerModule.Application.Features.Queries.PortalPermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionGroup;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class PortalPermissionsController 
    {
	    [HttpGet("get-portal-permissions/{permissionGroupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortalPermissionGroupList))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissions(string permissionGroupName)
        {
            var response = await mediator.Send(new PortalPermissionGroupGetItemsQuery(permissionGroupName));
            return Ok(response.Value);
        }

        [HttpPost("update-portal-permission")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdatePortalPermission(UpdatePortalPermissionGroupRequest request)
        {
            var response = await mediator.Send(new UpdatePortalPermissionGroupCommand(
                request.PortalPermissionName,
                request.PermissionGroupName,
                request.PortalPermissionTypeId,
                new PortalPermissionGroupDto()
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

