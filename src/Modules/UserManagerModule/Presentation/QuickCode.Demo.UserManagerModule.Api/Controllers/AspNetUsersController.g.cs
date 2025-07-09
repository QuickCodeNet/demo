using QuickCode.Demo.Common.Mediator;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using QuickCode.Demo.UserManagerModule.Application.Features;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AspNetUsersDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await mediator.Send(new AspNetUsersListQuery(page, size));
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await mediator.Send(new AspNetUsersTotalItemCountQuery());
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AspNetUsersDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(string id)
        {
            var response = await mediator.Send(new AspNetUsersGetItemQuery(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AspNetUsersDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(AspNetUsersDto model)
        {
            var response = await mediator.Send(new AspNetUsersInsertCommand(model));
            if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(string id, AspNetUsersDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await mediator.Send(new AspNetUsersUpdateCommand(id, model));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var response = await mediator.Send(new AspNetUsersDeleteItemCommand(id));
            if (response.Code == 404)
            {
                var notFoundMessage = $"Id: '{id}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("get-user/{aspNetUsersEmail}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AspNetUsersGetUserResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserAsync(string aspNetUsersEmail)
        {
            var response = await mediator.Send(new AspNetUsersGetUserQuery(aspNetUsersEmail));
            if (response.Code == 404)
            {
                var notFoundMessage = $"AspNetUsersEmail: '{aspNetUsersEmail}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{aspNetUsersId}/asp-net-user-roles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AspNetUsersGetAspNetUserRolesForAspNetUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserRolesForAspNetUsersAsync(string aspNetUsersId)
        {
            var response = await mediator.Send(new AspNetUsersGetAspNetUserRolesForAspNetUsersQuery(aspNetUsersId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"AspNetUsersId: '{aspNetUsersId}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{aspNetUsersId}/asp-net-user-roles/{aspNetUserRolesUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AspNetUsersGetAspNetUserRolesForAspNetUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserRolesForAspNetUsersDetailsAsync(string aspNetUsersId, string aspNetUserRolesUserId)
        {
            var response = await mediator.Send(new AspNetUsersGetAspNetUserRolesForAspNetUsersDetailsQuery(aspNetUsersId, aspNetUserRolesUserId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"AspNetUsersId: '{aspNetUsersId}', AspNetUserRolesUserId: '{aspNetUserRolesUserId}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{aspNetUsersId}/asp-net-user-claims")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AspNetUsersGetAspNetUserClaimsForAspNetUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserClaimsForAspNetUsersAsync(string aspNetUsersId)
        {
            var response = await mediator.Send(new AspNetUsersGetAspNetUserClaimsForAspNetUsersQuery(aspNetUsersId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"AspNetUsersId: '{aspNetUsersId}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{aspNetUsersId}/asp-net-user-claims/{aspNetUserClaimsId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AspNetUsersGetAspNetUserClaimsForAspNetUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserClaimsForAspNetUsersDetailsAsync(string aspNetUsersId, int aspNetUserClaimsId)
        {
            var response = await mediator.Send(new AspNetUsersGetAspNetUserClaimsForAspNetUsersDetailsQuery(aspNetUsersId, aspNetUserClaimsId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"AspNetUsersId: '{aspNetUsersId}', AspNetUserClaimsId: '{aspNetUserClaimsId}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{aspNetUsersId}/asp-net-user-tokens")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AspNetUsersGetAspNetUserTokensForAspNetUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserTokensForAspNetUsersAsync(string aspNetUsersId)
        {
            var response = await mediator.Send(new AspNetUsersGetAspNetUserTokensForAspNetUsersQuery(aspNetUsersId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"AspNetUsersId: '{aspNetUsersId}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{aspNetUsersId}/asp-net-user-tokens/{aspNetUserTokensUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AspNetUsersGetAspNetUserTokensForAspNetUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserTokensForAspNetUsersDetailsAsync(string aspNetUsersId, string aspNetUserTokensUserId)
        {
            var response = await mediator.Send(new AspNetUsersGetAspNetUserTokensForAspNetUsersDetailsQuery(aspNetUsersId, aspNetUserTokensUserId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"AspNetUsersId: '{aspNetUsersId}', AspNetUserTokensUserId: '{aspNetUserTokensUserId}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{aspNetUsersId}/asp-net-user-logins")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AspNetUsersGetAspNetUserLoginsForAspNetUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserLoginsForAspNetUsersAsync(string aspNetUsersId)
        {
            var response = await mediator.Send(new AspNetUsersGetAspNetUserLoginsForAspNetUsersQuery(aspNetUsersId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"AspNetUsersId: '{aspNetUsersId}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{aspNetUsersId}/asp-net-user-logins/{aspNetUserLoginsLoginProvider}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AspNetUsersGetAspNetUserLoginsForAspNetUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAspNetUserLoginsForAspNetUsersDetailsAsync(string aspNetUsersId, string aspNetUserLoginsLoginProvider)
        {
            var response = await mediator.Send(new AspNetUsersGetAspNetUserLoginsForAspNetUsersDetailsQuery(aspNetUsersId, aspNetUserLoginsLoginProvider));
            if (response.Code == 404)
            {
                var notFoundMessage = $"AspNetUsersId: '{aspNetUsersId}', AspNetUserLoginsLoginProvider: '{aspNetUserLoginsLoginProvider}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{aspNetUsersId}/refresh-tokens")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AspNetUsersGetRefreshTokensForAspNetUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetRefreshTokensForAspNetUsersAsync(string aspNetUsersId)
        {
            var response = await mediator.Send(new AspNetUsersGetRefreshTokensForAspNetUsersQuery(aspNetUsersId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"AspNetUsersId: '{aspNetUsersId}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }

        [HttpGet("{aspNetUsersId}/refresh-tokens/{refreshTokensId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AspNetUsersGetRefreshTokensForAspNetUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetRefreshTokensForAspNetUsersDetailsAsync(string aspNetUsersId, int refreshTokensId)
        {
            var response = await mediator.Send(new AspNetUsersGetRefreshTokensForAspNetUsersDetailsQuery(aspNetUsersId, refreshTokensId));
            if (response.Code == 404)
            {
                var notFoundMessage = $"AspNetUsersId: '{aspNetUsersId}', RefreshTokensId: '{refreshTokensId}' not found in AspNetUsers Table";
                logger.LogWarning($"List Error: '{notFoundMessage}''");
                return NotFound(notFoundMessage);
            }
            else if (response.Code != 0)
            {
                var errorMessage = $"Response Code: {response.Code}, Message: {response.Message}";
                logger.LogError($"List Error: '{errorMessage}''");
                return BadRequest(errorMessage);
            }

            return Ok(response.Value);
        }
    }
}