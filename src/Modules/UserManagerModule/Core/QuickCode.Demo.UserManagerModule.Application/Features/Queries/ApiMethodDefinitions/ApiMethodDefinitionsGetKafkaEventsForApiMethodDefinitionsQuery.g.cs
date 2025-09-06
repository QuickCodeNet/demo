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
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsQuery : IRequest<Response<List<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsResponseDto>>>
    {
        public string ApiMethodDefinitionsKey { get; set; }

        public ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsQuery(string apiMethodDefinitionsKey)
        {
            this.ApiMethodDefinitionsKey = apiMethodDefinitionsKey;
        }

        public class ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsHandler : IRequestHandler<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsQuery, Response<List<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsResponseDto>>>
        {
            private readonly ILogger<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsHandler> _logger;
            private readonly IApiMethodDefinitionsRepository _repository;
            public ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsHandler(ILogger<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsHandler> logger, IApiMethodDefinitionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsResponseDto>>> Handle(ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsAsync(request.ApiMethodDefinitionsKey);
                return returnValue.ToResponse();
            }
        }
    }
}