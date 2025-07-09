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
    public class ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsDetailsQuery : IRequest<Response<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsResponseDto>>
    {
        public int ApiMethodDefinitionsId { get; set; }
        public string KafkaEventsTopicName { get; set; }

        public ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsDetailsQuery(int apiMethodDefinitionsId, string kafkaEventsTopicName)
        {
            this.ApiMethodDefinitionsId = apiMethodDefinitionsId;
            this.KafkaEventsTopicName = kafkaEventsTopicName;
        }

        public class ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsDetailsHandler : IRequestHandler<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsDetailsQuery, Response<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsResponseDto>>
        {
            private readonly ILogger<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsDetailsHandler> _logger;
            private readonly IApiMethodDefinitionsRepository _repository;
            public ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsDetailsHandler(ILogger<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsDetailsHandler> logger, IApiMethodDefinitionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsResponseDto>> Handle(ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApiMethodDefinitionsGetKafkaEventsForApiMethodDefinitionsDetailsAsync(request.ApiMethodDefinitionsId, request.KafkaEventsTopicName);
                return returnValue.ToResponse();
            }
        }
    }
}