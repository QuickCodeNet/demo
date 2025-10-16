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
    public class GetApiPermissionGroupsForApiMethodDefinitionsQuery : IRequest<Response<List<GetApiPermissionGroupsForApiMethodDefinitionsResponseDto>>>
    {
        public string ApiMethodDefinitionsKey { get; set; }

        public GetApiPermissionGroupsForApiMethodDefinitionsQuery(string apiMethodDefinitionsKey)
        {
            this.ApiMethodDefinitionsKey = apiMethodDefinitionsKey;
        }

        public class GetApiPermissionGroupsForApiMethodDefinitionsHandler : IRequestHandler<GetApiPermissionGroupsForApiMethodDefinitionsQuery, Response<List<GetApiPermissionGroupsForApiMethodDefinitionsResponseDto>>>
        {
            private readonly ILogger<GetApiPermissionGroupsForApiMethodDefinitionsHandler> _logger;
            private readonly IApiMethodDefinitionRepository _repository;
            public GetApiPermissionGroupsForApiMethodDefinitionsHandler(ILogger<GetApiPermissionGroupsForApiMethodDefinitionsHandler> logger, IApiMethodDefinitionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetApiPermissionGroupsForApiMethodDefinitionsResponseDto>>> Handle(GetApiPermissionGroupsForApiMethodDefinitionsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetApiPermissionGroupsForApiMethodDefinitionsAsync(request.ApiMethodDefinitionsKey);
                return returnValue.ToResponse();
            }
        }
    }
}