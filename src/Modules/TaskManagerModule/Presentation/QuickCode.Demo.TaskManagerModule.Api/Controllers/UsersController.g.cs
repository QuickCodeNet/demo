using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.TaskManagerModule.Application.Dtos.User;
using QuickCode.Demo.TaskManagerModule.Application.Services.User;
using QuickCode.Demo.TaskManagerModule.Domain.Enums;

namespace QuickCode.Demo.TaskManagerModule.Api.Controllers
{
    public partial class UsersController : QuickCodeBaseApiController
    {
        private readonly IUserService service;
        private readonly ILogger<UsersController> logger;
        private readonly IServiceProvider serviceProvider;
        public UsersController(IUserService service, IServiceProvider serviceProvider, ILogger<UsersController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "User", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "User") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "User", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(UserDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "User") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, UserDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "User", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await service.DeleteItemAsync(id);
            if (HandleResponseError(response, logger, "User", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-active-users/{usersIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetActiveUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActiveUsersAsync(bool usersIsActive)
        {
            var response = await service.GetActiveUsersAsync(usersIsActive);
            if (HandleResponseError(response, logger, "User", $"UsersIsActive: '{usersIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-user-by-username/{usersUsername}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserByUsernameResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserByUsernameAsync(string usersUsername)
        {
            var response = await service.GetUserByUsernameAsync(usersUsername);
            if (HandleResponseError(response, logger, "User", $"UsersUsername: '{usersUsername}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-user-by-email/{usersEmail}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserByEmailResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetUserByEmailAsync(string usersEmail)
        {
            var response = await service.GetUserByEmailAsync(usersEmail);
            if (HandleResponseError(response, logger, "User", $"UsersEmail: '{usersEmail}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("exists-by-username/{usersUsername}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ExistsByUsernameAsync(string usersUsername)
        {
            var response = await service.ExistsByUsernameAsync(usersUsername);
            if (HandleResponseError(response, logger, "User", $"UsersUsername: '{usersUsername}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("exists-by-email/{usersEmail}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ExistsByEmailAsync(string usersEmail)
        {
            var response = await service.ExistsByEmailAsync(usersEmail);
            if (HandleResponseError(response, logger, "User", $"UsersEmail: '{usersEmail}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/task")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTasksForUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTasksForUsersAsync(int usersId)
        {
            var response = await service.GetTasksForUsersAsync(usersId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/task/{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTasksForUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTasksForUsersDetailsAsync(int usersId, int tasksId)
        {
            var response = await service.GetTasksForUsersDetailsAsync(usersId, tasksId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}', TasksId: '{tasksId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/project")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetProjectsForUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProjectsForUsersAsync(int usersId)
        {
            var response = await service.GetProjectsForUsersAsync(usersId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/project/{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProjectsForUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProjectsForUsersDetailsAsync(int usersId, int projectsId)
        {
            var response = await service.GetProjectsForUsersDetailsAsync(usersId, projectsId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}', ProjectsId: '{projectsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/task-comment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTaskCommentsForUsersResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTaskCommentsForUsersAsync(int usersId)
        {
            var response = await service.GetTaskCommentsForUsersAsync(usersId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{userId}/task-comment/{taskCommentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTaskCommentsForUsersResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTaskCommentsForUsersDetailsAsync(int usersId, int taskCommentsId)
        {
            var response = await service.GetTaskCommentsForUsersDetailsAsync(usersId, taskCommentsId);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}', TaskCommentsId: '{taskCommentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("activate-user/{usersId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ActivateUserAsync(int usersId, [FromBody] ActivateUserRequestDto updateRequest)
        {
            var response = await service.ActivateUserAsync(usersId, updateRequest);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("deactivate-user/{usersId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeactivateUserAsync(int usersId, [FromBody] DeactivateUserRequestDto updateRequest)
        {
            var response = await service.DeactivateUserAsync(usersId, updateRequest);
            if (HandleResponseError(response, logger, "User", $"UsersId: '{usersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}