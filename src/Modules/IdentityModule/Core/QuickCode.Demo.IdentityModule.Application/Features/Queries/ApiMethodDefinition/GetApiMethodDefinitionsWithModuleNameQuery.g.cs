using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.IdentityModule.Domain.Entities;
using QuickCode.Demo.IdentityModule.Application.Interfaces.Repositories;
using QuickCode.Demo.IdentityModule.Application.Dtos.ApiMethodDefinition;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.ApiMethodDefinition
{
    public class GetApiMethodDefinitionsWithModuleNameQuery : IRequest<Response<List<GetApiMethodDefinitionsWithModuleNameResponseDto>>>
    {
        public string ApiMethodDefinitionModuleName { get; set; }

        public GetApiMethodDefinitionsWithModuleNameQuery(string apiMethodDefinitionModuleName)
        {
            this.ApiMethodDefinitionModuleName = apiMethodDefinitionModuleName;
        }

        public class GetApiMethodDefinitionsWithModuleNameHandler : IRequestHandler<GetApiMethodDefinitionsWithModuleNameQuery, Response<List<GetApiMethodDefinitionsWithModuleNameResponseDto>>>
        {
            private readonly ILogger<GetApiMethodDefinitionsWithModuleNameHandler> _logger;
            private readonly IApiMethodDefinitionRepository _repository;
            public GetApiMethodDefinitionsWithModuleNameHandler(ILogger<GetApiMethodDefinitionsWithModuleNameHandler> logger, IApiMethodDefinitionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetApiMethodDefinitionsWithModuleNameResponseDto>>> Handle(GetApiMethodDefinitionsWithModuleNameQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetApiMethodDefinitionsWithModuleNameAsync(request.ApiMethodDefinitionModuleName);
                return returnValue.ToResponse();
            }
        }
    }
}