using QuickCode.Demo.UserManagerModule.Application.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Api.Application.Features.Queries.ApiPermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Features.ApiPermissionGroup;
using QuickCode.Demo.UserManagerModule.Application.Dtos.ApiPermissionGroup;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class ApiMethodDefinitionsController 
    {
	    [HttpGet("get-api-permissions/{permissionGroupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiModulePermissions))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetApiPermissions(string permissionGroupName)
        {
            var response = await mediator.Send(new ApiPermissionGroupGetItemsQuery(permissionGroupName));
            return Ok(response.Value);
        }
        
        [HttpPost("update-api-permission")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateApiPermission(UpdateApiPermissionGroupRequest request)
        {
            var response = await mediator.Send(new UpdateApiPermissionGroupCommand(
                request.PermissionGroupName, 
                request.ApiMethodDefinitionKey,
                new ApiPermissionGroupDto()
                {
                    PermissionGroupName = request.PermissionGroupName,
                    ApiMethodDefinitionKey = request.ApiMethodDefinitionKey,
                    IsActive = request.Value == 1
                }));

            return Ok(response.Code == 0);
        }
    }
}

