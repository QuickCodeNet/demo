using QuickCode.Demo.UserManagerModule.Application.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Api.Application.Features.Queries.ApiPermissionGroups;
using QuickCode.Demo.UserManagerModule.Application.Features;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class ApiMethodDefinitionsController 
    {
	    [HttpGet("get-api-permissions/{permissionGroupId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiModulePermissions))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissions(int permissionGroupId)
        {
            var response = await mediator.Send(new ApiPermissionGroupsGetItemsQuery(permissionGroupId));
            return Ok(response.Value);
        }

        [HttpPost("update-api-permission")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		public async Task<IActionResult> UpdateApiPermission(UpdateApiPermissionGroupRequest request)
        {
			if (request.Value == 1)
            {
                var response = await mediator.Send(new ApiPermissionGroupsInsertCommand(new ApiPermissionGroupsDto()
                {
                    PermissionGroupId = request.PermissionGroupId,
                    ApiMethodDefinitionId = request.ApiMethodDefinitionId
                }));
                
                return Ok(response.Code == 0);
            }
            else
            {
                var filterResponse = await mediator.Send(new ApiPermissionGroupsGetApiPermissionGroupQuery(request.PermissionGroupId, request.ApiMethodDefinitionId));

                if (filterResponse.Value.Any())
                {
                    var deleteItem = filterResponse.Value.First();
                    var deleteResponse = await mediator.Send(new ApiPermissionGroupsDeleteCommand(new ApiPermissionGroupsDto()
                    {
                        PermissionGroupId =  deleteItem.PermissionGroupId,
                        ApiMethodDefinitionId = deleteItem.ApiMethodDefinitionId,
                        Id = deleteItem.Id
                    }));

                    return Ok(deleteResponse.Value);
                }
            }

            return Ok(false);
        }
    }
}

