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
	    [HttpGet("get-portal-permissions/{permissionGroupId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortalPermissionGroupList))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPermissions(int permissionGroupId)
        {
            var response = await mediator.Send(new PortalPermissionGroupsGetItemsQuery(permissionGroupId));
            return Ok(response.Value);
        }

        [HttpPost("update-portal-permission")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		public async Task<IActionResult> UpdatePortalPermission(UpdatePortalPermissionGroupRequest request)
        {
			if (request.Value == 1)
            {
                var response = await mediator.Send(new PortalPermissionGroupsInsertCommand(new PortalPermissionGroupsDto()
                {
                    PermissionGroupId = request.PermissionGroupId,
                    PortalPermissionId = request.PortalPermissionId,
                    PortalPermissionTypeId = request.PortalPermissionTypeId
                }));

                return Ok(response.Code == 0);
            }
            else
            {
                var filterResponse = await mediator.Send(new PortalPermissionGroupsGetPortalPermissionGroupQuery(request.PortalPermissionId, request.PermissionGroupId, request.PortalPermissionTypeId));

                if (filterResponse.Value.Any())
                {
                    var deleteItem = filterResponse.Value.First();
                    var deleteResponse = await mediator.Send(new PortalPermissionGroupsDeleteCommand(new PortalPermissionGroupsDto()
                    {
                        PermissionGroupId = deleteItem.PermissionGroupId,
                        PortalPermissionId = deleteItem.PortalPermissionId,
                        PortalPermissionTypeId = deleteItem.PortalPermissionTypeId,
                        Id = deleteItem.Id
                    }));
					
                    return Ok(deleteResponse.Value);
                }

            }

            return Ok(false);
        }
    }
}

