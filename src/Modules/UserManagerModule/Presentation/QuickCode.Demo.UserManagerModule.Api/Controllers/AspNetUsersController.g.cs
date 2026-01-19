using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUser;
using QuickCode.Demo.UserManagerModule.Application.Features.AspNetUser;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Api.Controllers
{
    public partial class AspNetUsersController : QuickCodeBaseApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<AspNetUsersController> logger;
        private readonly IServiceProvider serviceProvider;
        public AspNetUsersController(IMediator mediator, IServiceProvider serviceProvider, ILogger<AspNetUsersController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AspNetUserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await mediator.Send(new ListAspNetUserQuery(page, size));
            if (HandleResponseError(response, logger, "AspNetUser", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new TotalCountAspNetUserQuery());
            if (HandleResponseError(response, logger, "AspNetUser") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AspNetUserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string id)
        {
            var response = await mediator.Send(new GetItemAspNetUserQuery(id));
            if (HandleResponseError(response, logger, "AspNetUser", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AspNetUserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(AspNetUserDto model)
        {
            var response = await mediator.Send(new InsertAspNetUserCommand(model));
            if (HandleResponseError(response, logger, "AspNetUser") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string id, AspNetUserDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new UpdateAspNetUserCommand(id, model));
            if (HandleResponseError(response, logger, "AspNetUser", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var response = await mediator.Send(new DeleteItemAspNetUserCommand(id));
            if (HandleResponseError(response, logger, "AspNetUser", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-user/{aspNetUsersEmail}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserAsync(string aspNetUsersEmail)
        {
            var response = await mediator.Send(new GetUserQuery(aspNetUsersEmail));
            if (HandleResponseError(response, logger, "AspNetUser", $"AspNetUsersEmail: '{aspNetUsersEmail}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetUserId}/refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetRefreshTokensForAspNetUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetRefreshTokensForAspNetUsersAsync(string aspNetUsersId)
        {
            var response = await mediator.Send(new GetRefreshTokensForAspNetUsersQuery(aspNetUsersId));
            if (HandleResponseError(response, logger, "AspNetUser", $"AspNetUsersId: '{aspNetUsersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetUserId}/refresh-token/{refreshTokenId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetRefreshTokensForAspNetUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetRefreshTokensForAspNetUsersDetailsAsync(string aspNetUsersId, int refreshTokensId)
        {
            var response = await mediator.Send(new GetRefreshTokensForAspNetUsersDetailsQuery(aspNetUsersId, refreshTokensId));
            if (HandleResponseError(response, logger, "AspNetUser", $"AspNetUsersId: '{aspNetUsersId}', RefreshTokensId: '{refreshTokensId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetUserId}/asp-net-user-role")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetAspNetUserRolesForAspNetUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserRolesForAspNetUsersAsync(string aspNetUsersId)
        {
            var response = await mediator.Send(new GetAspNetUserRolesForAspNetUsersQuery(aspNetUsersId));
            if (HandleResponseError(response, logger, "AspNetUser", $"AspNetUsersId: '{aspNetUsersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetUserId}/asp-net-user-role/{aspNetUserRoleUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAspNetUserRolesForAspNetUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserRolesForAspNetUsersDetailsAsync(string aspNetUsersId, string aspNetUserRolesUserId)
        {
            var response = await mediator.Send(new GetAspNetUserRolesForAspNetUsersDetailsQuery(aspNetUsersId, aspNetUserRolesUserId));
            if (HandleResponseError(response, logger, "AspNetUser", $"AspNetUsersId: '{aspNetUsersId}', AspNetUserRolesUserId: '{aspNetUserRolesUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetUserId}/asp-net-user-claim")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetAspNetUserClaimsForAspNetUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserClaimsForAspNetUsersAsync(string aspNetUsersId)
        {
            var response = await mediator.Send(new GetAspNetUserClaimsForAspNetUsersQuery(aspNetUsersId));
            if (HandleResponseError(response, logger, "AspNetUser", $"AspNetUsersId: '{aspNetUsersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetUserId}/asp-net-user-claim/{aspNetUserClaimId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAspNetUserClaimsForAspNetUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserClaimsForAspNetUsersDetailsAsync(string aspNetUsersId, int aspNetUserClaimsId)
        {
            var response = await mediator.Send(new GetAspNetUserClaimsForAspNetUsersDetailsQuery(aspNetUsersId, aspNetUserClaimsId));
            if (HandleResponseError(response, logger, "AspNetUser", $"AspNetUsersId: '{aspNetUsersId}', AspNetUserClaimsId: '{aspNetUserClaimsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetUserId}/asp-net-user-login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetAspNetUserLoginsForAspNetUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserLoginsForAspNetUsersAsync(string aspNetUsersId)
        {
            var response = await mediator.Send(new GetAspNetUserLoginsForAspNetUsersQuery(aspNetUsersId));
            if (HandleResponseError(response, logger, "AspNetUser", $"AspNetUsersId: '{aspNetUsersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetUserId}/asp-net-user-login/{aspNetUserLoginLoginProvider}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAspNetUserLoginsForAspNetUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserLoginsForAspNetUsersDetailsAsync(string aspNetUsersId, string aspNetUserLoginsLoginProvider)
        {
            var response = await mediator.Send(new GetAspNetUserLoginsForAspNetUsersDetailsQuery(aspNetUsersId, aspNetUserLoginsLoginProvider));
            if (HandleResponseError(response, logger, "AspNetUser", $"AspNetUsersId: '{aspNetUsersId}', AspNetUserLoginsLoginProvider: '{aspNetUserLoginsLoginProvider}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetUserId}/asp-net-user-token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetAspNetUserTokensForAspNetUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserTokensForAspNetUsersAsync(string aspNetUsersId)
        {
            var response = await mediator.Send(new GetAspNetUserTokensForAspNetUsersQuery(aspNetUsersId));
            if (HandleResponseError(response, logger, "AspNetUser", $"AspNetUsersId: '{aspNetUsersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{aspNetUserId}/asp-net-user-token/{aspNetUserTokenUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAspNetUserTokensForAspNetUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserTokensForAspNetUsersDetailsAsync(string aspNetUsersId, string aspNetUserTokensUserId)
        {
            var response = await mediator.Send(new GetAspNetUserTokensForAspNetUsersDetailsQuery(aspNetUsersId, aspNetUserTokensUserId));
            if (HandleResponseError(response, logger, "AspNetUser", $"AspNetUsersId: '{aspNetUsersId}', AspNetUserTokensUserId: '{aspNetUserTokensUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}