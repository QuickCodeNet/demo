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
    public class TopicWorkflowsTopicWorkflowsGetWorkflowsQuery : IRequest<Response<List<TopicWorkflowsGetWorkflowsResponseDto>>>
    {
        public string TopicWorkflowsKafkaEventsTopicName { get; set; }

        public TopicWorkflowsTopicWorkflowsGetWorkflowsQuery(string topicWorkflowsKafkaEventsTopicName)
        {
            this.TopicWorkflowsKafkaEventsTopicName = topicWorkflowsKafkaEventsTopicName;
        }

        public class TopicWorkflowsTopicWorkflowsGetWorkflowsHandler : IRequestHandler<TopicWorkflowsTopicWorkflowsGetWorkflowsQuery, Response<List<TopicWorkflowsGetWorkflowsResponseDto>>>
        {
            private readonly ILogger<TopicWorkflowsTopicWorkflowsGetWorkflowsHandler> _logger;
            private readonly ITopicWorkflowsRepository _repository;
            public TopicWorkflowsTopicWorkflowsGetWorkflowsHandler(ILogger<TopicWorkflowsTopicWorkflowsGetWorkflowsHandler> logger, ITopicWorkflowsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<TopicWorkflowsGetWorkflowsResponseDto>>> Handle(TopicWorkflowsTopicWorkflowsGetWorkflowsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.TopicWorkflowsGetWorkflowsAsync(request.TopicWorkflowsKafkaEventsTopicName);
                return returnValue.ToResponse();
            }
        }
    }
}