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
    public partial class TaskCommentService : ITaskCommentService
    {
        private readonly ILogger<TaskCommentService> _logger;
        private readonly ITaskCommentRepository _repository;
        public TaskCommentService(ILogger<TaskCommentService> logger, ITaskCommentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<TaskCommentDto>> InsertAsync(TaskCommentDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(TaskCommentDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, TaskCommentDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<TaskCommentDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<TaskCommentDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetCommentsForTaskResponseDto>>> GetCommentsForTaskAsync(int taskCommentsTaskId)
        {
            var returnValue = await _repository.GetCommentsForTaskAsync(taskCommentsTaskId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> DeleteCommentAsync(int taskCommentsId)
        {
            var returnValue = await _repository.DeleteCommentAsync(taskCommentsId);
            return returnValue.ToResponse();
        }
    }
}