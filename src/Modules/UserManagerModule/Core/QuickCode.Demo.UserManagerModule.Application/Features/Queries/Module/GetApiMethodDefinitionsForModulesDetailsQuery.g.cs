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
using QuickCode.Demo.UserManagerModule.Application.Dtos.Module;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.Module
{
    public class GetApiMethodDefinitionsForModulesDetailsQuery : IRequest<Response<GetApiMethodDefinitionsForModulesResponseDto>>
    {
        public string ModulesName { get; set; }
        public string ApiMethodDefinitionsKey { get; set; }

        public GetApiMethodDefinitionsForModulesDetailsQuery(string modulesName, string apiMethodDefinitionsKey)
        {
            this.ModulesName = modulesName;
            this.ApiMethodDefinitionsKey = apiMethodDefinitionsKey;
        }

        public class GetApiMethodDefinitionsForModulesDetailsHandler : IRequestHandler<GetApiMethodDefinitionsForModulesDetailsQuery, Response<GetApiMethodDefinitionsForModulesResponseDto>>
        {
            private readonly ILogger<GetApiMethodDefinitionsForModulesDetailsHandler> _logger;
            private readonly IModuleRepository _repository;
            public GetApiMethodDefinitionsForModulesDetailsHandler(ILogger<GetApiMethodDefinitionsForModulesDetailsHandler> logger, IModuleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetApiMethodDefinitionsForModulesResponseDto>> Handle(GetApiMethodDefinitionsForModulesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetApiMethodDefinitionsForModulesDetailsAsync(request.ModulesName, request.ApiMethodDefinitionsKey);
                return returnValue.ToResponse();
            }
        }
    }
}