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
    public partial interface ITaskService
    {
        Task<Response<TaskDto>> InsertAsync(TaskDto request);
        Task<Response<bool>> DeleteAsync(TaskDto request);
        Task<Response<bool>> UpdateAsync(int id, TaskDto request);
        Task<Response<List<TaskDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<TaskDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetTasksByUserIdResponseDto>>> GetTasksByUserIdAsync(int tasksUserId);
        Task<Response<List<GetTasksByStatusResponseDto>>> GetTasksByStatusAsync(TaskStatus tasksStatus);
        Task<Response<List<GetTasksByPriorityResponseDto>>> GetTasksByPriorityAsync(short tasksPriority);
        Task<Response<List<GetTasksDueTodayResponseDto>>> GetTasksDueTodayAsync(DateTime tasksDueDate);
        Task<Response<List<SearchTasksResponseDto>>> SearchTasksAsync(string tasksTitle);
        Task<Response<List<GetTaskCommentsForTasksResponseDto>>> GetTaskCommentsForTasksAsync(int tasksId);
        Task<Response<GetTaskCommentsForTasksResponseDto>> GetTaskCommentsForTasksDetailsAsync(int tasksId, int taskCommentsId);
        Task<Response<int>> UpdateTaskStatusAsync(int tasksId, UpdateTaskStatusRequestDto updateRequest);
        Task<Response<int>> UpdateTaskPriorityAsync(int tasksId, UpdateTaskPriorityRequestDto updateRequest);
        Task<Response<int>> DeleteTaskAsync(int tasksId);
    }
}