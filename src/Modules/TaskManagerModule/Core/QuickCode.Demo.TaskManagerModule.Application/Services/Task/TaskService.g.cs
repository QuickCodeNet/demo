using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.TaskManagerModule.Domain.Entities;
using QuickCode.Demo.TaskManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.TaskManagerModule.Application.Dtos.Task;
using QuickCode.Demo.TaskManagerModule.Domain.Enums;

namespace QuickCode.Demo.TaskManagerModule.Application.Services.Task
{
    public partial class TaskService : ITaskService
    {
        private readonly ILogger<TaskService> _logger;
        private readonly ITaskRepository _repository;
        public TaskService(ILogger<TaskService> logger, ITaskRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<TaskDto>> InsertAsync(TaskDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(TaskDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, TaskDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<TaskDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<TaskDto>> GetItemAsync(int id)
        {
            var returnValue = await _repository.GetByPkAsync(id);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int id)
        {
            var deleteItem = await _repository.GetByPkAsync(id);
            if (deleteItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.DeleteAsync(deleteItem.Value);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> TotalItemCountAsync()
        {
            var returnValue = await _repository.CountAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetTasksByUserIdResponseDto>>> GetTasksByUserIdAsync(int tasksUserId)
        {
            var returnValue = await _repository.GetTasksByUserIdAsync(tasksUserId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetTasksByStatusResponseDto>>> GetTasksByStatusAsync(TaskStatus tasksStatus)
        {
            var returnValue = await _repository.GetTasksByStatusAsync(tasksStatus);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetTasksByPriorityResponseDto>>> GetTasksByPriorityAsync(short tasksPriority)
        {
            var returnValue = await _repository.GetTasksByPriorityAsync(tasksPriority);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetTasksDueTodayResponseDto>>> GetTasksDueTodayAsync(DateTime tasksDueDate)
        {
            var returnValue = await _repository.GetTasksDueTodayAsync(tasksDueDate);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SearchTasksResponseDto>>> SearchTasksAsync(string tasksTitle)
        {
            var returnValue = await _repository.SearchTasksAsync(tasksTitle);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetTaskCommentsForTasksResponseDto>>> GetTaskCommentsForTasksAsync(int tasksId)
        {
            var returnValue = await _repository.GetTaskCommentsForTasksAsync(tasksId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetTaskCommentsForTasksResponseDto>> GetTaskCommentsForTasksDetailsAsync(int tasksId, int taskCommentsId)
        {
            var returnValue = await _repository.GetTaskCommentsForTasksDetailsAsync(tasksId, taskCommentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateTaskStatusAsync(int tasksId, UpdateTaskStatusRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateTaskStatusAsync(tasksId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateTaskPriorityAsync(int tasksId, UpdateTaskPriorityRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateTaskPriorityAsync(tasksId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> DeleteTaskAsync(int tasksId)
        {
            var returnValue = await _repository.DeleteTaskAsync(tasksId);
            return returnValue.ToResponse();
        }
    }
}