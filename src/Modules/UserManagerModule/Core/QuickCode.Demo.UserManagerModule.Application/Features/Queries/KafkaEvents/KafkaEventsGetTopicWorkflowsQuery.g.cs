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
    public class KafkaEventsGetTopicWorkflowsQuery : IRequest<Response<List<KafkaEventsGetTopicWorkflowsResponseDto>>>
    {
        public string KafkaEventsTopicName { get; set; }
        public HttpMethodType ApiMethodDefinitionsHttpMethod { get; set; }

        public KafkaEventsGetTopicWorkflowsQuery(string kafkaEventsTopicName, HttpMethodType apiMethodDefinitionsHttpMethod)
        {
            this.KafkaEventsTopicName = kafkaEventsTopicName;
            this.ApiMethodDefinitionsHttpMethod = apiMethodDefinitionsHttpMethod;
        }

        public class KafkaEventsGetTopicWorkflowsHandler : IRequestHandler<KafkaEventsGetTopicWorkflowsQuery, Response<List<KafkaEventsGetTopicWorkflowsResponseDto>>>
        {
            private readonly ILogger<KafkaEventsGetTopicWorkflowsHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsGetTopicWorkflowsHandler(ILogger<KafkaEventsGetTopicWorkflowsHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<KafkaEventsGetTopicWorkflowsResponseDto>>> Handle(KafkaEventsGetTopicWorkflowsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.KafkaEventsGetTopicWorkflowsAsync(request.KafkaEventsTopicName, request.ApiMethodDefinitionsHttpMethod);
                return returnValue.ToResponse();
            }
        }
    }
}