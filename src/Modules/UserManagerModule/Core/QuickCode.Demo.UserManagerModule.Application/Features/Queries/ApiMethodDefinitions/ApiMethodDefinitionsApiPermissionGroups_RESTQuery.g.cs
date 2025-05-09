using System;
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
    public class ApiMethodDefinitionsApiMethodDefinitionsApiPermissionGroups_RESTQuery : IRequest<Response<List<ApiMethodDefinitionsApiPermissionGroups_RESTResponseDto>>>
    {
        public int ApiMethodDefinitionsId { get; set; }

        public ApiMethodDefinitionsApiMethodDefinitionsApiPermissionGroups_RESTQuery(int apiMethodDefinitionsId)
        {
            this.ApiMethodDefinitionsId = apiMethodDefinitionsId;
        }

        public class ApiMethodDefinitionsApiMethodDefinitionsApiPermissionGroups_RESTHandler : IRequestHandler<ApiMethodDefinitionsApiMethodDefinitionsApiPermissionGroups_RESTQuery, Response<List<ApiMethodDefinitionsApiPermissionGroups_RESTResponseDto>>>
        {
            private readonly ILogger<ApiMethodDefinitionsApiMethodDefinitionsApiPermissionGroups_RESTHandler> _logger;
            private readonly IApiMethodDefinitionsRepository _repository;
            public ApiMethodDefinitionsApiMethodDefinitionsApiPermissionGroups_RESTHandler(ILogger<ApiMethodDefinitionsApiMethodDefinitionsApiPermissionGroups_RESTHandler> logger, IApiMethodDefinitionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApiMethodDefinitionsApiPermissionGroups_RESTResponseDto>>> Handle(ApiMethodDefinitionsApiMethodDefinitionsApiPermissionGroups_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApiMethodDefinitionsApiPermissionGroups_RESTAsync(request.ApiMethodDefinitionsId);
                return returnValue.ToResponse();
            }
        }
    }
}