using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.TaskManagerModule.Domain.Entities;
using QuickCode.Demo.TaskManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.TaskManagerModule.Application.Dtos.TaskComment;
using QuickCode.Demo.TaskManagerModule.Domain.Enums;

namespace QuickCode.Demo.TaskManagerModule.Application.Services.TaskComment
{
    public partial interface ITaskCommentService
    {
        Task<Response<TaskCommentDto>> InsertAsync(TaskCommentDto request);
        Task<Response<bool>> DeleteAsync(TaskCommentDto request);
        Task<Response<bool>> UpdateAsync(int id, TaskCommentDto request);
        Task<Response<List<TaskCommentDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<TaskCommentDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetCommentsForTaskResponseDto>>> GetCommentsForTaskAsync(int taskCommentsTaskId);
        Task<Response<int>> DeleteCommentAsync(int taskCommentsId);
    }
}