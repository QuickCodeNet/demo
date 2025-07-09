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
    public class KafkaEventsGetTopicWorkflowsForKafkaEventsQuery : IRequest<Response<List<KafkaEventsGetTopicWorkflowsForKafkaEventsResponseDto>>>
    {
        public string KafkaEventsTopicName { get; set; }

        public KafkaEventsGetTopicWorkflowsForKafkaEventsQuery(string kafkaEventsTopicName)
        {
            this.KafkaEventsTopicName = kafkaEventsTopicName;
        }

        public class KafkaEventsGetTopicWorkflowsForKafkaEventsHandler : IRequestHandler<KafkaEventsGetTopicWorkflowsForKafkaEventsQuery, Response<List<KafkaEventsGetTopicWorkflowsForKafkaEventsResponseDto>>>
        {
            private readonly ILogger<KafkaEventsGetTopicWorkflowsForKafkaEventsHandler> _logger;
            private readonly IKafkaEventsRepository _repository;
            public KafkaEventsGetTopicWorkflowsForKafkaEventsHandler(ILogger<KafkaEventsGetTopicWorkflowsForKafkaEventsHandler> logger, IKafkaEventsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<KafkaEventsGetTopicWorkflowsForKafkaEventsResponseDto>>> Handle(KafkaEventsGetTopicWorkflowsForKafkaEventsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.KafkaEventsGetTopicWorkflowsForKafkaEventsAsync(request.KafkaEventsTopicName);
                return returnValue.ToResponse();
            }
        }
    }
}