using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.TaskManagerModule.Domain.Entities;
using QuickCode.Demo.TaskManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.TaskManagerModule.Application.Dtos.Project;
using QuickCode.Demo.TaskManagerModule.Domain.Enums;

namespace QuickCode.Demo.TaskManagerModule.Application.Services.Project
{
    public partial interface IProjectService
    {
        Task<Response<ProjectDto>> InsertAsync(ProjectDto request);
        Task<Response<bool>> DeleteAsync(ProjectDto request);
        Task<Response<bool>> UpdateAsync(int id, ProjectDto request);
        Task<Response<List<ProjectDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<ProjectDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetProjectsByUserIdResponseDto>>> GetProjectsByUserIdAsync(int projectsUserId);
        Task<Response<List<GetProjectsDueSoonResponseDto>>> GetProjectsDueSoonAsync();
        Task<Response<List<SearchProjectsResponseDto>>> SearchProjectsAsync(string projectsName);
        Task<Response<int>> UpdateProjectEndDateAsync(int projectsId, UpdateProjectEndDateRequestDto updateRequest);
        Task<Response<int>> DeleteProjectAsync(int projectsId);
    }
}