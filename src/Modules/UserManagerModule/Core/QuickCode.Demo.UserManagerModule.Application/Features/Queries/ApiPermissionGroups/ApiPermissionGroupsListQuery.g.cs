using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class ApiPermissionGroupsListQuery : IRequest<Response<List<ApiPermissionGroupsDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ApiPermissionGroupsListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ApiPermissionGroupsListHandler : IRequestHandler<ApiPermissionGroupsListQuery, Response<List<ApiPermissionGroupsDto>>>
        {
            private readonly ILogger<ApiPermissionGroupsListHandler> _logger;
            private readonly IApiPermissionGroupsRepository _repository;
            public ApiPermissionGroupsListHandler(ILogger<ApiPermissionGroupsListHandler> logger, IApiPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApiPermissionGroupsDto>>> Handle(ApiPermissionGroupsListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}