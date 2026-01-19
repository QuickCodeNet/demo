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
    public class GetApiMethodAccessGrantsForApiMethodDefinitionsDetailsQuery : IRequest<Response<GetApiMethodAccessGrantsForApiMethodDefinitionsResponseDto>>
    {
        public string ApiMethodDefinitionsKey { get; set; }
        public string ApiMethodAccessGrantsPermissionGroupName { get; set; }

        public GetApiMethodAccessGrantsForApiMethodDefinitionsDetailsQuery(string apiMethodDefinitionsKey, string apiMethodAccessGrantsPermissionGroupName)
        {
            this.ApiMethodDefinitionsKey = apiMethodDefinitionsKey;
            this.ApiMethodAccessGrantsPermissionGroupName = apiMethodAccessGrantsPermissionGroupName;
        }

        public class GetApiMethodAccessGrantsForApiMethodDefinitionsDetailsHandler : IRequestHandler<GetApiMethodAccessGrantsForApiMethodDefinitionsDetailsQuery, Response<GetApiMethodAccessGrantsForApiMethodDefinitionsResponseDto>>
        {
            private readonly ILogger<GetApiMethodAccessGrantsForApiMethodDefinitionsDetailsHandler> _logger;
            private readonly IApiMethodDefinitionRepository _repository;
            public GetApiMethodAccessGrantsForApiMethodDefinitionsDetailsHandler(ILogger<GetApiMethodAccessGrantsForApiMethodDefinitionsDetailsHandler> logger, IApiMethodDefinitionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetApiMethodAccessGrantsForApiMethodDefinitionsResponseDto>> Handle(GetApiMethodAccessGrantsForApiMethodDefinitionsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetApiMethodAccessGrantsForApiMethodDefinitionsDetailsAsync(request.ApiMethodDefinitionsKey, request.ApiMethodAccessGrantsPermissionGroupName);
                return returnValue.ToResponse();
            }
        }
    }
}