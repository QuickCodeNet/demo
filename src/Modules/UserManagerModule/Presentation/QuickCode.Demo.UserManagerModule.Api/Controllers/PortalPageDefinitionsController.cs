using QuickCode.Demo.UserManagerModule.Application.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickCode.Demo.UserManagerModule.Application.Features.Queries.PortalPageAccessGrant;
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPageAccessGrant;
using QuickCode.Demo.UserManagerModule.Application.Features.PortalPageAccessGrant;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class PortalPageDefinitionsController 
    {
	    [HttpGet("get-portal-page-definitions/{permissionGroupName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PortalPageAccessGrantList))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPortalPageDefinitions(string permissionGroupName)
        {
            var response = await mediator.Send(new PortalPageAccessGrantGetItemsQuery(permissionGroupName));
            return Ok(response.Value);
        }

        [HttpPost("update-portal-page-permission")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdatePortalPagePermission(UpdatePortalPageAccessGrantRequest request)
        {
            var response = await mediator.Send(new UpdatePortalPageAccessGrantCommand(
                request.PermissionGroupName,
                request.PortalPagePermissionName,
                request.PortalPagePermissionType,
                new PortalPageAccessGrantDto()
                {
                    PermissionGroupName = request.PermissionGroupName,
                    PortalPageDefinitionKey = request.PortalPagePermissionName,
                    PageAction = request.PortalPagePermissionType,
                    ModifiedBy = request.Value == 1 ? ModificationType.User : ModificationType.UserDisabled,
                    IsActive = request.Value == 1
                }));

            return Ok(response.Code == 0);
        }
    }
}

