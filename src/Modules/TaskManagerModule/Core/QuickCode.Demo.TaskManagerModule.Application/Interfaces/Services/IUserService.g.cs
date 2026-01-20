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
    public partial interface IUserService
    {
        Task<Response<UserDto>> InsertAsync(UserDto request);
        Task<Response<bool>> DeleteAsync(UserDto request);
        Task<Response<bool>> UpdateAsync(int id, UserDto request);
        Task<Response<List<UserDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<UserDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetActiveUsersResponseDto>>> GetActiveUsersAsync(bool usersIsActive);
        Task<Response<GetUserByUsernameResponseDto>> GetUserByUsernameAsync(string usersUsername);
        Task<Response<GetUserByEmailResponseDto>> GetUserByEmailAsync(string usersEmail);
        Task<Response<bool>> ExistsByUsernameAsync(string usersUsername);
        Task<Response<bool>> ExistsByEmailAsync(string usersEmail);
        Task<Response<List<GetTasksForUsersResponseDto>>> GetTasksForUsersAsync(int usersId);
        Task<Response<GetTasksForUsersResponseDto>> GetTasksForUsersDetailsAsync(int usersId, int tasksId);
        Task<Response<List<GetProjectsForUsersResponseDto>>> GetProjectsForUsersAsync(int usersId);
        Task<Response<GetProjectsForUsersResponseDto>> GetProjectsForUsersDetailsAsync(int usersId, int projectsId);
        Task<Response<List<GetTaskCommentsForUsersResponseDto>>> GetTaskCommentsForUsersAsync(int usersId);
        Task<Response<GetTaskCommentsForUsersResponseDto>> GetTaskCommentsForUsersDetailsAsync(int usersId, int taskCommentsId);
        Task<Response<int>> ActivateUserAsync(int usersId, ActivateUserRequestDto updateRequest);
        Task<Response<int>> DeactivateUserAsync(int usersId, DeactivateUserRequestDto updateRequest);
    }
}