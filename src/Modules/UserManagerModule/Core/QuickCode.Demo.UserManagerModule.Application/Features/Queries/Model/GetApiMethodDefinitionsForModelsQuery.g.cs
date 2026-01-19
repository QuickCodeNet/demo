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
    public class GetApiMethodDefinitionsForModelsQuery : IRequest<Response<List<GetApiMethodDefinitionsForModelsResponseDto>>>
    {
        public string ModelsName { get; set; }

        public GetApiMethodDefinitionsForModelsQuery(string modelsName)
        {
            this.ModelsName = modelsName;
        }

        public class GetApiMethodDefinitionsForModelsHandler : IRequestHandler<GetApiMethodDefinitionsForModelsQuery, Response<List<GetApiMethodDefinitionsForModelsResponseDto>>>
        {
            private readonly ILogger<GetApiMethodDefinitionsForModelsHandler> _logger;
            private readonly IModelRepository _repository;
            public GetApiMethodDefinitionsForModelsHandler(ILogger<GetApiMethodDefinitionsForModelsHandler> logger, IModelRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetApiMethodDefinitionsForModelsResponseDto>>> Handle(GetApiMethodDefinitionsForModelsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetApiMethodDefinitionsForModelsAsync(request.ModelsName);
                return returnValue.ToResponse();
            }
        }
    }
}