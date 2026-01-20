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
    public partial class ProjectService : IProjectService
    {
        private readonly ILogger<ProjectService> _logger;
        private readonly IProjectRepository _repository;
        public ProjectService(ILogger<ProjectService> logger, IProjectRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<ProjectDto>> InsertAsync(ProjectDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(ProjectDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, ProjectDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<ProjectDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<ProjectDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetProjectsByUserIdResponseDto>>> GetProjectsByUserIdAsync(int projectsUserId)
        {
            var returnValue = await _repository.GetProjectsByUserIdAsync(projectsUserId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetProjectsDueSoonResponseDto>>> GetProjectsDueSoonAsync()
        {
            var returnValue = await _repository.GetProjectsDueSoonAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<SearchProjectsResponseDto>>> SearchProjectsAsync(string projectsName)
        {
            var returnValue = await _repository.SearchProjectsAsync(projectsName);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> UpdateProjectEndDateAsync(int projectsId, UpdateProjectEndDateRequestDto updateRequest)
        {
            var returnValue = await _repository.UpdateProjectEndDateAsync(projectsId, updateRequest);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> DeleteProjectAsync(int projectsId)
        {
            var returnValue = await _repository.DeleteProjectAsync(projectsId);
            return returnValue.ToResponse();
        }
    }
}