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
    public class KafkaEventsKafkaEventsGetTopicWorkflowsQuery : IRequest<Response<List<KafkaEventsGetTopicWorkflowsResponseDto>>>
    {
        public string KafkaEventsTopicName { get; set; }
        public string ApiMethodDefinitionsHttpMethod { get; set; }

        public KafkaEventsKafkaEventsGetTopicWorkflowsQuery(string kafkaEventsTopicName, string apiMethodDefinitionsHttpMethod)
        {
            this.KafkaEventsTopicName = kafkaEventsTopicName;
            this.ApiMethodDefinitionsHttpMethod = apiMethodDefinitionsHttpMethod;
        }

        public class KafkaEventsKafkaEventsGetTopicWorkflowsHandler : IRequestHandler<KafkaEventsKafkaEventsGetTopicWorkflowsQuery, Response<List<KafkaEventsGetTopicWorkflowsResponseDto>>>
        {
            private readonly ILogger<KafkaEventsKafkaEventsGetTopicWorkflowsHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsKafkaEventsGetTopicWorkflowsHandler(ILogger<KafkaEventsKafkaEventsGetTopicWorkflowsHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<KafkaEventsGetTopicWorkflowsResponseDto>>> Handle(KafkaEventsKafkaEventsGetTopicWorkflowsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.KafkaEventsGetTopicWorkflowsAsync(request.KafkaEventsTopicName, request.ApiMethodDefinitionsHttpMethod);
                return returnValue.ToResponse();
            }
        }
    }
}