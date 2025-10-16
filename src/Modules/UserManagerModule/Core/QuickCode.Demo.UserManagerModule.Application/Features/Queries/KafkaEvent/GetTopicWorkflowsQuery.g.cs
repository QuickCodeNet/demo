﻿using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos.KafkaEvent;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.KafkaEvent
{
    public class GetTopicWorkflowsQuery : IRequest<Response<List<GetTopicWorkflowsResponseDto>>>
    {
        public string KafkaEventsTopicName { get; set; }
        public HttpMethodType ApiMethodDefinitionsHttpMethod { get; set; }

        public GetTopicWorkflowsQuery(string kafkaEventsTopicName, HttpMethodType apiMethodDefinitionsHttpMethod)
        {
            this.KafkaEventsTopicName = kafkaEventsTopicName;
            this.ApiMethodDefinitionsHttpMethod = apiMethodDefinitionsHttpMethod;
        }

        public class GetTopicWorkflowsHandler : IRequestHandler<GetTopicWorkflowsQuery, Response<List<GetTopicWorkflowsResponseDto>>>
        {
            private readonly ILogger<GetTopicWorkflowsHandler> _logger;
            private readonly IKafkaEventRepository _repository;
            public GetTopicWorkflowsHandler(ILogger<GetTopicWorkflowsHandler> logger, IKafkaEventRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetTopicWorkflowsResponseDto>>> Handle(GetTopicWorkflowsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetTopicWorkflowsAsync(request.KafkaEventsTopicName, request.ApiMethodDefinitionsHttpMethod);
                return returnValue.ToResponse();
            }
        }
    }
}