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
    public class GetItemApiMethodDefinitionQuery : IRequest<Response<ApiMethodDefinitionDto>>
    {
        public string Key { get; set; }

        public GetItemApiMethodDefinitionQuery(string key)
        {
            this.Key = key;
        }

        public class GetItemApiMethodDefinitionHandler : IRequestHandler<GetItemApiMethodDefinitionQuery, Response<ApiMethodDefinitionDto>>
        {
            private readonly ILogger<GetItemApiMethodDefinitionHandler> _logger;
            private readonly IApiMethodDefinitionRepository _repository;
            public GetItemApiMethodDefinitionHandler(ILogger<GetItemApiMethodDefinitionHandler> logger, IApiMethodDefinitionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApiMethodDefinitionDto>> Handle(GetItemApiMethodDefinitionQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Key);
                return returnValue.ToResponse();
            }
        }
    }
}