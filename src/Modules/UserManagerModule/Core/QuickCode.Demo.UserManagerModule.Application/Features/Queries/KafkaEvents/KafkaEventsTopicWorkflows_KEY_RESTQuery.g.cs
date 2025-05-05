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
    public class KafkaEventsKafkaEventsTopicWorkflows_KEY_RESTQuery : IRequest<Response<KafkaEventsTopicWorkflows_KEY_RESTResponseDto>>
    {
        public string KafkaEventsTopicName { get; set; }
        public int TopicWorkflowsId { get; set; }

        public KafkaEventsKafkaEventsTopicWorkflows_KEY_RESTQuery(string kafkaEventsTopicName, int topicWorkflowsId)
        {
            this.KafkaEventsTopicName = kafkaEventsTopicName;
            this.TopicWorkflowsId = topicWorkflowsId;
        }

        public class KafkaEventsKafkaEventsTopicWorkflows_KEY_RESTHandler : IRequestHandler<KafkaEventsKafkaEventsTopicWorkflows_KEY_RESTQuery, Response<KafkaEventsTopicWorkflows_KEY_RESTResponseDto>>
        {
            private readonly ILogger<KafkaEventsKafkaEventsTopicWorkflows_KEY_RESTHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsKafkaEventsTopicWorkflows_KEY_RESTHandler(ILogger<KafkaEventsKafkaEventsTopicWorkflows_KEY_RESTHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<KafkaEventsTopicWorkflows_KEY_RESTResponseDto>> Handle(KafkaEventsKafkaEventsTopicWorkflows_KEY_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.KafkaEventsTopicWorkflows_KEY_RESTAsync(request.KafkaEventsTopicName, request.TopicWorkflowsId);
                return returnValue.ToResponse();
            }
        }
    }
}