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
    public class ApiMethodDefinitionsTotalItemCountQuery : IRequest<Response<int>>
    {
        public ApiMethodDefinitionsTotalItemCountQuery()
        {
        }

        public class ApiMethodDefinitionsTotalItemCountHandler : IRequestHandler<ApiMethodDefinitionsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<ApiMethodDefinitionsTotalItemCountHandler> _logger;
            private readonly IApiMethodDefinitionsRepository _repository;
            public ApiMethodDefinitionsTotalItemCountHandler(ILogger<ApiMethodDefinitionsTotalItemCountHandler> logger, IApiMethodDefinitionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(ApiMethodDefinitionsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}