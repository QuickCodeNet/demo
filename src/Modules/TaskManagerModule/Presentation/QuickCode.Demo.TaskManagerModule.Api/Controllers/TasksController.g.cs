using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.TaskManagerModule.Application.Dtos.Task;
using QuickCode.Demo.TaskManagerModule.Application.Services.Task;
using QuickCode.Demo.TaskManagerModule.Domain.Enums;

namespace QuickCode.Demo.TaskManagerModule.Api.Controllers
{
    public partial class TasksController : QuickCodeBaseApiController
    {
        private readonly ITaskService service;
        private readonly ILogger<TasksController> logger;
        private readonly IServiceProvider serviceProvider;
        public TasksController(ITaskService service, IServiceProvider serviceProvider, ILogger<TasksController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TaskDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Task", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "Task") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Task", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(TaskDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "Task") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, TaskDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "Task", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "Task", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-tasks-by-user-id/{tasksUserId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTasksByUserIdResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTasksByUserIdAsync(int tasksUserId)
        {
            var response = await service.GetTasksByUserIdAsync(tasksUserId);
            if (HandleResponseError(response, logger, "Task", $"TasksUserId: '{tasksUserId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-tasks-by-status/{tasksStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTasksByStatusResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTasksByStatusAsync(TaskStatus tasksStatus)
        {
            var response = await service.GetTasksByStatusAsync(tasksStatus);
            if (HandleResponseError(response, logger, "Task", $"TasksStatus: '{tasksStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-tasks-by-priority/{tasksPriority:short}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTasksByPriorityResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTasksByPriorityAsync(short tasksPriority)
        {
            var response = await service.GetTasksByPriorityAsync(tasksPriority);
            if (HandleResponseError(response, logger, "Task", $"TasksPriority: '{tasksPriority}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-tasks-due-today/{tasksDueDate:DateTime}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTasksDueTodayResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTasksDueTodayAsync(DateTime tasksDueDate)
        {
            var response = await service.GetTasksDueTodayAsync(tasksDueDate);
            if (HandleResponseError(response, logger, "Task", $"TasksDueDate: '{tasksDueDate}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("search-tasks/{tasksTitle}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SearchTasksResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> SearchTasksAsync(string tasksTitle)
        {
            var response = await service.SearchTasksAsync(tasksTitle);
            if (HandleResponseError(response, logger, "Task", $"TasksTitle: '{tasksTitle}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{taskId}/task-comment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTaskCommentsForTasksResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTaskCommentsForTasksAsync(int tasksId)
        {
            var response = await service.GetTaskCommentsForTasksAsync(tasksId);
            if (HandleResponseError(response, logger, "Task", $"TasksId: '{tasksId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{taskId}/task-comment/{taskCommentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTaskCommentsForTasksResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTaskCommentsForTasksDetailsAsync(int tasksId, int taskCommentsId)
        {
            var response = await service.GetTaskCommentsForTasksDetailsAsync(tasksId, taskCommentsId);
            if (HandleResponseError(response, logger, "Task", $"TasksId: '{tasksId}', TaskCommentsId: '{taskCommentsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-task-status/{tasksId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateTaskStatusAsync(int tasksId, [FromBody] UpdateTaskStatusRequestDto updateRequest)
        {
            var response = await service.UpdateTaskStatusAsync(tasksId, updateRequest);
            if (HandleResponseError(response, logger, "Task", $"TasksId: '{tasksId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("update-task-priority/{tasksId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateTaskPriorityAsync(int tasksId, [FromBody] UpdateTaskPriorityRequestDto updateRequest)
        {
            var response = await service.UpdateTaskPriorityAsync(tasksId, updateRequest);
            if (HandleResponseError(response, logger, "Task", $"TasksId: '{tasksId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("delete-task/{tasksId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteTaskAsync(int tasksId)
        {
            var response = await service.DeleteTaskAsync(tasksId);
            if (HandleResponseError(response, logger, "Task", $"TasksId: '{tasksId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}