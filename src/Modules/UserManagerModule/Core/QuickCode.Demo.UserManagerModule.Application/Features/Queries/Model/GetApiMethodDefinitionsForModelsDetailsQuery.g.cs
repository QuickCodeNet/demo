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
using QuickCode.Demo.UserManagerModule.Application.Dtos.Model;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.Model
{
    public class GetApiMethodDefinitionsForModelsDetailsQuery : IRequest<Response<GetApiMethodDefinitionsForModelsResponseDto>>
    {
        public string ModelsName { get; set; }
        public string ApiMethodDefinitionsKey { get; set; }

        public GetApiMethodDefinitionsForModelsDetailsQuery(string modelsName, string apiMethodDefinitionsKey)
        {
            this.ModelsName = modelsName;
            this.ApiMethodDefinitionsKey = apiMethodDefinitionsKey;
        }

        public class GetApiMethodDefinitionsForModelsDetailsHandler : IRequestHandler<GetApiMethodDefinitionsForModelsDetailsQuery, Response<GetApiMethodDefinitionsForModelsResponseDto>>
        {
            private readonly ILogger<GetApiMethodDefinitionsForModelsDetailsHandler> _logger;
            private readonly IModelRepository _repository;
            public GetApiMethodDefinitionsForModelsDetailsHandler(ILogger<GetApiMethodDefinitionsForModelsDetailsHandler> logger, IModelRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetApiMethodDefinitionsForModelsResponseDto>> Handle(GetApiMethodDefinitionsForModelsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetApiMethodDefinitionsForModelsDetailsAsync(request.ModelsName, request.ApiMethodDefinitionsKey);
                return returnValue.ToResponse();
            }
        }
    }
}