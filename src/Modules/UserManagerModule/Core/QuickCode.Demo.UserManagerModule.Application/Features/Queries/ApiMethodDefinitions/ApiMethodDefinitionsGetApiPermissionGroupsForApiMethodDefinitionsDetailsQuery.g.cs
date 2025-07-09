using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
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
    public class ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery : IRequest<Response<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsResponseDto>>
    {
        public int ApiMethodDefinitionsId { get; set; }
        public int ApiPermissionGroupsId { get; set; }

        public ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery(int apiMethodDefinitionsId, int apiPermissionGroupsId)
        {
            this.ApiMethodDefinitionsId = apiMethodDefinitionsId;
            this.ApiPermissionGroupsId = apiPermissionGroupsId;
        }

        public class ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsHandler : IRequestHandler<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery, Response<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsResponseDto>>
        {
            private readonly ILogger<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsHandler> _logger;
            private readonly IApiMethodDefinitionsRepository _repository;
            public ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsHandler(ILogger<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsHandler> logger, IApiMethodDefinitionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsResponseDto>> Handle(ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsAsync(request.ApiMethodDefinitionsId, request.ApiPermissionGroupsId);
                return returnValue.ToResponse();
            }
        }
    }
}