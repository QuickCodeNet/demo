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
    public class ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_KEY_RESTQuery : IRequest<Response<ApiMethodDefinitionsKafkaEvents_KEY_RESTResponseDto>>
    {
        public int ApiMethodDefinitionsId { get; set; }
        public string KafkaEventsTopicName { get; set; }

        public ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_KEY_RESTQuery(int apiMethodDefinitionsId, string kafkaEventsTopicName)
        {
            this.ApiMethodDefinitionsId = apiMethodDefinitionsId;
            this.KafkaEventsTopicName = kafkaEventsTopicName;
        }

        public class ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_KEY_RESTHandler : IRequestHandler<ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_KEY_RESTQuery, Response<ApiMethodDefinitionsKafkaEvents_KEY_RESTResponseDto>>
        {
            private readonly ILogger<ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_KEY_RESTHandler> _logger;
            private readonly IApiMethodDefinitionsRepository _repository;
            public ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_KEY_RESTHandler(ILogger<ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_KEY_RESTHandler> logger, IApiMethodDefinitionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApiMethodDefinitionsKafkaEvents_KEY_RESTResponseDto>> Handle(ApiMethodDefinitionsApiMethodDefinitionsKafkaEvents_KEY_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApiMethodDefinitionsKafkaEvents_KEY_RESTAsync(request.ApiMethodDefinitionsId, request.KafkaEventsTopicName);
                return returnValue.ToResponse();
            }
        }
    }
}