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
    public class KafkaEventsGetTopicWorkflowsForKafkaEventsDetailsQuery : IRequest<Response<KafkaEventsGetTopicWorkflowsForKafkaEventsResponseDto>>
    {
        public string KafkaEventsTopicName { get; set; }
        public int TopicWorkflowsId { get; set; }

        public KafkaEventsGetTopicWorkflowsForKafkaEventsDetailsQuery(string kafkaEventsTopicName, int topicWorkflowsId)
        {
            this.KafkaEventsTopicName = kafkaEventsTopicName;
            this.TopicWorkflowsId = topicWorkflowsId;
        }

        public class KafkaEventsGetTopicWorkflowsForKafkaEventsDetailsHandler : IRequestHandler<KafkaEventsGetTopicWorkflowsForKafkaEventsDetailsQuery, Response<KafkaEventsGetTopicWorkflowsForKafkaEventsResponseDto>>
        {
            private readonly ILogger<KafkaEventsGetTopicWorkflowsForKafkaEventsDetailsHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsGetTopicWorkflowsForKafkaEventsDetailsHandler(ILogger<KafkaEventsGetTopicWorkflowsForKafkaEventsDetailsHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<KafkaEventsGetTopicWorkflowsForKafkaEventsResponseDto>> Handle(KafkaEventsGetTopicWorkflowsForKafkaEventsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.KafkaEventsGetTopicWorkflowsForKafkaEventsDetailsAsync(request.KafkaEventsTopicName, request.TopicWorkflowsId);
                return returnValue.ToResponse();
            }
        }
    }
}