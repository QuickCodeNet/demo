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
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsQuery : IRequest<Response<List<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsResponseDto>>>
    {
        public string ApiMethodDefinitionsKey { get; set; }

        public ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsQuery(string apiMethodDefinitionsKey)
        {
            this.ApiMethodDefinitionsKey = apiMethodDefinitionsKey;
        }

        public class ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsHandler : IRequestHandler<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsQuery, Response<List<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsResponseDto>>>
        {
            private readonly ILogger<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsHandler> _logger;
            private readonly IApiMethodDefinitionsRepository _repository;
            public ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsHandler(ILogger<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsHandler> logger, IApiMethodDefinitionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsResponseDto>>> Handle(ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsAsync(request.ApiMethodDefinitionsKey);
                return returnValue.ToResponse();
            }
        }
    }
}