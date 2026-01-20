using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.TaskManagerModule.Domain.Entities;
using QuickCode.Demo.TaskManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.TaskManagerModule.Application.Dtos.User;
using QuickCode.Demo.TaskManagerModule.Domain.Enums;

namespace QuickCode.Demo.TaskManagerModule.Application.Services.User
{
    public partial class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _repository;
        public UserService(ILogger<UserService> logger, IUserRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<UserDto>> InsertAsync(UserDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(UserDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, UserDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<UserDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<UserDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetActiveUsersResponseDto>>> GetActiveUsersAsync(bool usersIsActive)
        {
            var returnValue = await _repository.GetActiveUsersAsync(usersIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetUserByUsernameResponseDto>> GetUserByUsernameAsync(string usersUsername)
        {
            var returnValue = await _repository.GetUserByUsernameAsync(usersUsername);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetUserByEmailResponseDto>> GetUserByEmailAsync(string usersEmail)
        {
            var returnValue = await _repository.GetUserByEmailAsync(usersEmail);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> ExistsByUsernameAsync(string usersUsername)
        {
            var returnValue = await _repository.ExistsByUsernameAsync(usersUsername);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> ExistsByEmailAsync(string usersEmail)
        {
            var returnValue = await _repository.ExistsByEmailAsync(usersEmail);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetTasksForUsersResponseDto>>> GetTasksForUsersAsync(int usersId)
        {
            var returnValue = await _repository.GetTasksForUsersAsync(usersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetTasksForUsersResponseDto>> GetTasksForUsersDetailsAsync(int usersId, int tasksId)
        {
            var returnValue = await _repository.GetTasksForUsersDetailsAsync(usersId, tasksId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetProjectsForUsersResponseDto>>> GetProjectsForUsersAsync(int usersId)
        {
            var returnValue = await _repository.GetProjectsForUsersAsync(usersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetProjectsForUsersResponseDto>> GetProjectsForUsersDetailsAsync(int usersId, int projectsId)
        {
            var returnValue = await _repository.GetProjectsForUsersDetailsAsync(usersId, projectsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetTaskCommentsForUsersResponseDto>>> GetTaskCommentsForUsersAsync(int usersId)
        {
            var returnValue = await _repository.GetTaskCommentsForUsersAsync(usersId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetTaskCommentsForUsersResponseDto>> GetTaskCommentsForUsersDetailsAsync(int usersId, int taskCommentsId)
        {
            var returnValue = await _repository.GetTaskCommentsForUsersDetailsAsync(usersId, taskCommentsId);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> ActivateUserAsync(int usersId, ActivateUserRequestDto updateRequest)
        {
            var returnValue = await _repository.ActivateUserAsync(usersId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> DeactivateUserAsync(int usersId, DeactivateUserRequestDto updateRequest)
        {
            var returnValue = await _repository.DeactivateUserAsync(usersId, updateRequest);
            return returnValue.ToResponse();
        }
    }
}