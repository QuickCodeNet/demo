using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.TaskManagerModule.Application.Dtos.Project;
using QuickCode.Demo.TaskManagerModule.Application.Services.Project;
using QuickCode.Demo.TaskManagerModule.Domain.Enums;

namespace QuickCode.Demo.TaskManagerModule.Api.Controllers
{
    public partial class ProjectsController : QuickCodeBaseApiController
    {
        private readonly IProjectService service;
        private readonly ILogger<ProjectsController> logger;
        private readonly IServiceProvider serviceProvider;
        public ProjectsController(IProjectService service, IServiceProvider serviceProvider, ILogger<ProjectsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProjectDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Project", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "Project") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Project", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ProjectDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "Project") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, ProjectDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "Project", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "Project", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-projects-by-user-id/{projectsUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetProjectsByUserIdResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProjectsByUserIdAsync(int projectsUserId)
        {
            var response = await service.GetProjectsByUserIdAsync(projectsUserId);
            if (HandleResponseError(response, logger, "Project", $"ProjectsUserId: '{projectsUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-projects-due-soon")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetProjectsDueSoonResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProjectsDueSoonAsync()
        {
            var response = await service.GetProjectsDueSoonAsync();
            if (HandleResponseError(response, logger, "Project", $"") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("search-projects/{projectsName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SearchProjectsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> SearchProjectsAsync(string projectsName)
        {
            var response = await service.SearchProjectsAsync(projectsName);
            if (HandleResponseError(response, logger, "Project", $"ProjectsName: '{projectsName}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-project-end-date/{projectsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateProjectEndDateAsync(int projectsId, [FromBody] UpdateProjectEndDateRequestDto updateRequest)
        {
            var response = await service.UpdateProjectEndDateAsync(projectsId, updateRequest);
            if (HandleResponseError(response, logger, "Project", $"ProjectsId: '{projectsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("delete-project/{projectsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteProjectAsync(int projectsId)
        {
            var response = await service.DeleteProjectAsync(projectsId);
            if (HandleResponseError(response, logger, "Project", $"ProjectsId: '{projectsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}