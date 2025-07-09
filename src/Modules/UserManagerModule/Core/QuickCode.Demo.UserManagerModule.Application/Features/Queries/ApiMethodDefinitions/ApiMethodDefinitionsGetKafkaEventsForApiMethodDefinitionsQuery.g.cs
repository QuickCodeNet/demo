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

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsQuery : IRequest<Response<List<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsResponseDto>>>
    {
        public int ApiMethodDefinitionsId { get; set; }

        public ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsQuery(int apiMethodDefinitionsId)
        {
            this.ApiMethodDefinitionsId = apiMethodDefinitionsId;
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
                var returnValue = await _repository.ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsAsync(request.ApiMethodDefinitionsId);
                return returnValue.ToResponse();
            }
        }
    }
}