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
    public class TopicWorkflowsGetWorkflowsQuery : IRequest<Response<List<TopicWorkflowsGetWorkflowsResponseDto>>>
    {
        public string TopicWorkflowsKafkaEventsTopicName { get; set; }

        public TopicWorkflowsGetWorkflowsQuery(string topicWorkflowsKafkaEventsTopicName)
        {
            this.TopicWorkflowsKafkaEventsTopicName = topicWorkflowsKafkaEventsTopicName;
        }

        public class TopicWorkflowsGetWorkflowsHandler : IRequestHandler<TopicWorkflowsGetWorkflowsQuery, Response<List<TopicWorkflowsGetWorkflowsResponseDto>>>
        {
            private readonly ILogger<TopicWorkflowsGetWorkflowsHandler> _logger;
            private readonly ITopicWorkflowsRepository _repository;
            public TopicWorkflowsGetWorkflowsHandler(ILogger<TopicWorkflowsGetWorkflowsHandler> logger, ITopicWorkflowsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<TopicWorkflowsGetWorkflowsResponseDto>>> Handle(TopicWorkflowsGetWorkflowsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.TopicWorkflowsGetWorkflowsAsync(request.TopicWorkflowsKafkaEventsTopicName);
                return returnValue.ToResponse();
            }
        }
    }
}