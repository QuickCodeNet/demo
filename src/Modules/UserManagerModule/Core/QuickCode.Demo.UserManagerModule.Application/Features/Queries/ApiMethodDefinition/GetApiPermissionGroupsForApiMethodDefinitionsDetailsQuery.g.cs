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
using QuickCode.Demo.UserManagerModule.Application.Dtos.ApiMethodDefinition;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.ApiMethodDefinition
{
    public class GetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery : IRequest<Response<GetApiPermissionGroupsForApiMethodDefinitionsResponseDto>>
    {
        public string ApiMethodDefinitionsKey { get; set; }
        public string ApiPermissionGroupsPermissionGroupName { get; set; }

        public GetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery(string apiMethodDefinitionsKey, string apiPermissionGroupsPermissionGroupName)
        {
            this.ApiMethodDefinitionsKey = apiMethodDefinitionsKey;
            this.ApiPermissionGroupsPermissionGroupName = apiPermissionGroupsPermissionGroupName;
        }

        public class GetApiPermissionGroupsForApiMethodDefinitionsDetailsHandler : IRequestHandler<GetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery, Response<GetApiPermissionGroupsForApiMethodDefinitionsResponseDto>>
        {
            private readonly ILogger<GetApiPermissionGroupsForApiMethodDefinitionsDetailsHandler> _logger;
            private readonly IApiMethodDefinitionRepository _repository;
            public GetApiPermissionGroupsForApiMethodDefinitionsDetailsHandler(ILogger<GetApiPermissionGroupsForApiMethodDefinitionsDetailsHandler> logger, IApiMethodDefinitionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetApiPermissionGroupsForApiMethodDefinitionsResponseDto>> Handle(GetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetApiPermissionGroupsForApiMethodDefinitionsDetailsAsync(request.ApiMethodDefinitionsKey, request.ApiPermissionGroupsPermissionGroupName);
                return returnValue.ToResponse();
            }
        }
    }
}