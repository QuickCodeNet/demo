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
    public class ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_RESTQuery : IRequest<Response<List<ApiMethodDefinitionsKafkaEvents_RESTResponseDto>>>
    {
        public int ApiMethodDefinitionsId { get; set; }

        public ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_RESTQuery(int apiMethodDefinitionsId)
        {
            this.ApiMethodDefinitionsId = apiMethodDefinitionsId;
        }

        public class ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_RESTHandler : IRequestHandler<ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_RESTQuery, Response<List<ApiMethodDefinitionsKafkaEvents_RESTResponseDto>>>
        {
            private readonly ILogger<ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_RESTHandler> _logger;
            private readonly IApiMethodDefinitionsRepository _repository;
            public ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_RESTHandler(ILogger<ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_RESTHandler> logger, IApiMethodDefinitionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApiMethodDefinitionsKafkaEvents_RESTResponseDto>>> Handle(ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApiMethodDefinitionsKafkaEvents_RESTAsync(request.ApiMethodDefinitionsId);
                return returnValue.ToResponse();
            }
        }
    }
}