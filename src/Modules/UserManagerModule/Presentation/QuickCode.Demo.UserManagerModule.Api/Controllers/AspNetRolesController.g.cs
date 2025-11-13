using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetRole;
using QuickCode.Demo.UserManagerModule.Application.Features.AspNetRole;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class AspNetRolesController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<AspNetRolesController> logger;
        private readonly IServiceProvider serviceProvider;
        public AspNetRolesController(IMediator mediator, IServiceProvider serviceProvider, ILogger<AspNetRolesController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AspNetRoleDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListAspNetRoleQuery(page, size));
            if (HandleResponseError(response, logger, "AspNetRole", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountAspNetRoleQuery());
            if (HandleResponseError(response, logger, "AspNetRole") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AspNetRoleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string id)
        {
            var response = await mediator.Send(new GetItemAspNetRoleQuery(id));
            if (HandleResponseError(response, logger, "AspNetRole", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AspNetRoleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(AspNetRoleDto model)
        {
            var response = await mediator.Send(new InsertAspNetRoleCommand(model));
            if (HandleResponseError(response, logger, "AspNetRole") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string id, AspNetRoleDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new UpdateAspNetRoleCommand(id, model));
            if (HandleResponseError(response, logger, "AspNetRole", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var response = await mediator.Send(new DeleteItemAspNetRoleCommand(id));
            if (HandleResponseError(response, logger, "AspNetRole", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetRoleId}/asp-net-user-role")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetAspNetUserRolesForAspNetRolesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserRolesForAspNetRolesAsync(string aspNetRolesId)
        {
            var response = await mediator.Send(new GetAspNetUserRolesForAspNetRolesQuery(aspNetRolesId));
            if (HandleResponseError(response, logger, "AspNetRole", $"AspNetRolesId: '{aspNetRolesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetRoleId}/asp-net-user-role/{aspNetUserRoleUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAspNetUserRolesForAspNetRolesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserRolesForAspNetRolesDetailsAsync(string aspNetRolesId, string aspNetUserRolesUserId)
        {
            var response = await mediator.Send(new GetAspNetUserRolesForAspNetRolesDetailsQuery(aspNetRolesId, aspNetUserRolesUserId));
            if (HandleResponseError(response, logger, "AspNetRole", $"AspNetRolesId: '{aspNetRolesId}', AspNetUserRolesUserId: '{aspNetUserRolesUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetRoleId}/asp-net-role-claim")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetAspNetRoleClaimsForAspNetRolesResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetRoleClaimsForAspNetRolesAsync(string aspNetRolesId)
        {
            var response = await mediator.Send(new GetAspNetRoleClaimsForAspNetRolesQuery(aspNetRolesId));
            if (HandleResponseError(response, logger, "AspNetRole", $"AspNetRolesId: '{aspNetRolesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetRoleId}/asp-net-role-claim/{aspNetRoleClaimId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAspNetRoleClaimsForAspNetRolesResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetRoleClaimsForAspNetRolesDetailsAsync(string aspNetRolesId, int aspNetRoleClaimsId)
        {
            var response = await mediator.Send(new GetAspNetRoleClaimsForAspNetRolesDetailsQuery(aspNetRolesId, aspNetRoleClaimsId));
            if (HandleResponseError(response, logger, "AspNetRole", $"AspNetRolesId: '{aspNetRolesId}', AspNetRoleClaimsId: '{aspNetRoleClaimsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}